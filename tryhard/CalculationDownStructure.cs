using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tryhard
{
    public enum StructureType { Kesson = 0, Monoleg = 1, Multileg = 2 };

    class StructureCortage
    {
        public StructureCortage () { }
    }

    static class Field
    {
        public static double yWater { get; set; } = 1;
        public static double dLocalWater { get; set; } = 30;
        public static double dGlobalWater { get; set; } = 60;
        public static double hWave50 { get; set; } = 6;
        public static double hWave001 { get; set; } = 12;
        public static double dIce { get; set; } = 3;
        public static double durabilityIce { get; set; } = 2;
        public static double diameterIce { get; set; } = 3000;
        public static double speedIce { get; set; } = 0.5;
        public static double groundDurability { get; set; } = 2500;
    }

    public class DownStructure
    {
        private double minTrCl { get; set; } = Field.hWave50 + 0.5;
        public StructureType Type { get; set; }
        public bool isCalculated { get; set; }
        public int countBC { get; set; } = 25;
        public int countSC { get; set; } = 1;
        public double wUpStructure { get; set; } = 100;
        public double pUpStructure { get; set; }
        public double dWallCell { get; set; } = 0.75;
        public double hStructure { get; set; }
        public double wCell { get; set; } = 20;
        public double hExCl { get; set; }
        public double hTrCl { get; set; }
        public double yMat { get; set; } = 2.5;
        public double yMatBallast { get; set; } = 1.6;
        public double Cost { get; set; }
        public BaseCell baseCell { get; set; } = new BaseCell();
        public Cell supportCell { get; set; } = new Cell();

        public DownStructure(StructureType type)
        {
            switch (type)
            {
                case StructureType.Kesson: countSC = 25; countBC = 25; break;
                case StructureType.Monoleg: countSC = 1; countBC = 25; break;
                case StructureType.Multileg: countSC = 4; countBC = 25; break;
            }
        }

        public bool CalculateDownStructure(double _pUpStructure)
        {
            isCalculated = true;
            pUpStructure = _pUpStructure;
            CalculateHeightStricture();
            supportCell.CalculateParameters(hExCl, hTrCl, wCell, dWallCell, yMat);
            double liftingPowerDiff = 0;
            bool isStability = false;
            while (!isStability)
            {
                baseCell.CalculateParameters(hExCl, hTrCl, wCell, dWallCell, yMat);
                if (CalculateEhouthBallast())                
                {
                    if (CheckGroundDurability() && CheckFlatShift())
                    {
                        isStability = true;
                    }
                    else if (!AddBaseCell())
                        return false;
                }
                else if (!AddBaseCell())
                    return false;
            }
            Cost = (countSC * supportCell.p + countBC * baseCell.p) * 120000.0;
            return true;
        }

        public void CalculateHeightStricture()
        {
            hExCl = 0.5 * Field.hWave001 + 0.5;
            hStructure = Field.dGlobalWater + hExCl;
            hTrCl = hStructure - Field.dGlobalWater * 0.4;
        }

        public bool CalculateEhouthBallast()
        {
            while (GetLiftingPower() <= 0)
            {
                if (ChangeTrCl())
                {
                    supportCell.CalculateParameters(hExCl, hTrCl, wCell, dWallCell, yMat);
                    if (!CalculateBallast()) return false;
                }
                else return false;
            }
            bool isStability = false;
            while (!isStability)
            {
                if (CalculateGNphi() > 0)
                    isStability = true;
                else
                {
                    if (ChangeTrCl())
                    {
                        supportCell.CalculateParameters(hExCl, hTrCl, wCell, dWallCell, yMat);
                        if (!CalculateBallast()) return false;
                        else isStability = true;
                    }
                    else return false;
                }
            }
            return true;
        }

        public bool ChangeTrCl()
        {
            if (hTrCl-- < minTrCl)
                return false;
            return true;
        }

        public bool CalculateBallast()
        {
            double liftingPower = GetLiftingPower();
            baseCell.pBallast = liftingPower / Convert.ToDouble(countBC);
            baseCell.dBallast = baseCell.pBallast / (baseCell.aBottom * yMatBallast);
            if (baseCell.dBallast > baseCell.h - baseCell.dCover - baseCell.dBottom) return false;
            return true;
        }
        //
        //Проверка на плоский сдвиг
        //
        public bool CheckFlatShift()
        {
            return (CalculateLongitudinalForce(Field.yWater) * Math.Tan(180 / Math.PI * 20)) / CalculateIceLoad() > 1;
        }
        //
        //Проверка прочности по грунту
        //
        public bool CheckGroundDurability()
        {
            double longitudinalForce = CalculateLongitudinalForce(Field.yWater);
            double aBaseCells = countBC * baseCell.a;
            double armPower = CalculateArmPower();
            double iceLoad = CalculateIceLoad();
            double momentFlexResistance = CalculateMomentFlexResistance();
            double sigma = -longitudinalForce / aBaseCells - (armPower * iceLoad) / momentFlexResistance;
            return Field.groundDurability / sigma > 1;
        }
        //
        //Рассчет ледовой нагрузки
        //
        public double CalculateIceLoad()
        {
            double dh = supportCell.wOutside / Field.dIce;
            double kb = 0;
            double kv = 0;
            double Fcp = 0;
            double Fbp = 0;
            CalculateKbKv(out kb, out kv, dh, Field.speedIce, Field.diameterIce);
            Fbp = 0.83 * kb * kv * Field.durabilityIce * Field.diameterIce * Field.dIce;
            Fcp = 0.00126 * Field.speedIce * Field.dIce * Math.Sqrt(Fbp * supportCell.a);
            return Math.Min(Fcp, Fbp);
        }

        //
        //Момент сопротивления изгибу
        //
        public double CalculateMomentFlexResistance()
        {
            double inertion = Math.Pow(Math.Sqrt(countBC) * supportCell.wOutside, 4) / 12.0;
            double ymax = (Math.Sqrt(countBC) / 2.0) * supportCell.wOutside;
            return inertion / ymax; 
        }

        public double CalculateLongitudinalForce(double _yWater)
        {
            return (-pUpStructure - countBC * baseCell.p - countSC * supportCell.p - CalculatingLiqBallast(_yWater) + CalculatingArchimedesForce(_yWater));
        }

        public double CalculatingLiqBallast(double _yWater)
        {
            return countBC * Math.Pow(baseCell.wOutside - 2 * baseCell.dWall, 2) * (baseCell.h - 2 * baseCell.dWall - baseCell.dBallast) * _yWater;
        }

        public double CalculatingArchimedesForce(double _yWater)
        {
            return (countBC * baseCell.v + countSC * (supportCell.v - supportCell.vTrCl)) * _yWater;
        }

        public void CalculateKbKv(out double kb, out double kv, double dh, double Vk, double Dk)
        {
            double Kl = 0;
            if (dh < 10)
            {
                if (dh < 0.3)
                    kb = 5.7;
                else if (dh < 1)
                    kb = -3 * dh + 6.6;
                else if (dh < 3)
                    kb = -0.3 * dh + 3.9;
                else
                    kb = -0.1 * dh + 3.3;
                Kl = 4;
            }
            else
            {
                if (dh < 30)
                {
                    kb = -0.04 * dh + 2.7;
                    Kl = 4;
                }
                else
                {
                    kb = 1.5;
                    Kl = 2;
                }
            }
            kv = Kv(Vk / (Kl * Dk));
        }

        private double Kv(double e)
        {
            double kv;
            if (e < 1E-7)
                kv = 0.1;
            else if (e < 2.4E-5)
                kv = 29289 * e + 0.0971;
            else if (e < 1E-4)
                kv = -2E-7 * Math.Pow(e, 2) + 5643.7 * e + 0.6785;
            else if (e < 5E-1)
                kv = 1;
            else if (e < 1E-3)
                kv = -400 * e + 1.2;
            else if (e < 1E-2)
                kv = 3888.9 * Math.Pow(e, 2) - 98.33 * e + 0.8944;
            else
                kv = 0.3;
            return kv;
        }

        public double CalculateArmPower()
        {
            return Field.dGlobalWater - (17.0 / 30.0) * Field.dIce;
        }

        //
        //Вычисление избыточной плавучести
        //
        public double GetLiftingPower()
        {
            return countSC * supportCell.GetLiftingPowerValue(Field.yWater) + 
                   countBC * baseCell.GetLiftingPowerValue(Field.yWater);
        }
        //
        //Вычисление остойчивости при транспортировке
        //
        public double CalculateGNphi()
        {
            double floatingStabilityInertiaMoment = CalculateFloatingStabilityInertiaMoment();
            double volume = CalculateVolume();
            double phi = CalculatePhi();
            double kb = CalculateKB();
            double kg = CalculateKG();
            return (floatingStabilityInertiaMoment / volume) * (1 + Math.Pow(Math.Tan(phi), 2)) + kb - kb;
        }

        public double CalculateKB()
        {
            double mvbaseCell = countBC * baseCell.v;
            double nvsupportCell = countSC * (supportCell.v - Math.Pow(supportCell.wOutside, 2) * hTrCl);
            return (mvbaseCell * (baseCell.h / 2.0) + nvsupportCell * (baseCell.h + (supportCell.h / 2.0))) / (mvbaseCell + nvsupportCell);
        }

        public double CalculateKG()
        {
            double mp_baseCell = countBC * baseCell.p;
            double mp_supportCell = countSC * supportCell.p;
            return (mp_baseCell * baseCell.kp + mp_supportCell * supportCell.kp) / (mp_baseCell + mp_supportCell);
        }

        public double CalculateFloatingStabilityInertiaMoment()
        {
            double inertialMoment = 0;
            switch(Type)
            {
                case StructureType.Kesson:
                    inertialMoment = Math.Sqrt(countSC) * Math.Pow(supportCell.wOutside, 4) * 
                                     CalculateSummIM(Convert.ToInt32(Math.Sqrt(countSC) - 1) / 2); break;
                case StructureType.Monoleg:
                    inertialMoment = Math.Pow(supportCell.wOutside, 4) / 12.0; break;
                case StructureType.Multileg: break;
                    inertialMoment = Math.Pow(wUpStructure - baseCell.wOutside, 2) * Math.Pow(supportCell.wOutside, 2) + 
                        Math.Pow(supportCell.wOutside, 4) / 3.0;
            }
            return inertialMoment;
        }

        public double CalculateVolume()
        {
            return (Convert.ToDouble(countBC) * baseCell.v + 
                    Convert.ToDouble(countSC) * supportCell.v - 
                    Convert.ToDouble(countSC) * Math.Pow(supportCell.wOutside, 2) * hTrCl);
        }

        public double CalculatePhi()
        {
            double phi = 0;
            if (Type == StructureType.Kesson || Type == StructureType.Multileg)
                phi = 2.0 * hTrCl / wUpStructure;
            else
                phi = hTrCl / supportCell.wOutside;
            phi = phi * (180 / Math.PI) * 0.925;
            return phi; 
        }

        private double CalculateSummIM(int upLimit)
        {
            double summ = 0;
            for (int i = 0; i <= upLimit; i++)
            {
                summ += Convert.ToDouble((12 * Math.Pow(i, 2) + 1)/6);
            }
            return summ - Convert.ToDouble(1 / 6);
        }

        public bool AddBaseCell()
        {
            countBC = Convert.ToInt32(Math.Pow(Math.Sqrt(countBC) + 2, 2));
            return countBC >= 144 ? false : true;
        }
    }

    public class Cell
    {
        public double a { get; set; }
        public double h { get; set; }
        public double v { get; set; }
        public double p { get; set; }
        public double kp { get; set; }
        public double vMat { get; set; }
        public double dWall { get; set; }
        public double vTrCl { get; set; }
        public double wInside { get; set; }
        public double wOutside { get; set; }
        public double aCSWall { get; set; }
        public double vMatWall { get; set; }
        public double pMatWall { get; set; }

        public virtual void CalculateParameters(double _hExCl, double _hTrCl, double _wOutside, double _dWall, double _yMat)
        {
            dWall = _dWall;
            h = _hExCl + Field.dGlobalWater * 0.6;
            wOutside = _wOutside;
            wInside = wOutside - 2 * dWall;
            a = wOutside * wOutside;
            aCSWall = a - wInside * wInside;
            vMatWall = aCSWall * h;
            v = a * h;
            vMat = vMatWall;
            vTrCl = a * _hTrCl;
            pMatWall = vMatWall * _yMat;
            p = pMatWall;
            kp = Field.dGlobalWater * 0.4 + h * 0.5;
        }

        public virtual double GetLiftingPowerValue(double yWater)
        {
            return (v - vTrCl) * yWater - p;
        } 
    }

    public class BaseCell : Cell
    {
        public double dCover { get; set; }
        public double aCover { get; set; }
        public double vMatCover { get; set; }
        public double pMatCover { get; set; }

        public double dBottom { get; set; }
        public double aBottom { get; set; }
        public double vMatBottom { get; set; }
        public double pMatBottom { get; set; }
        public double dBallast { get; set; }
        public double pBallast { get; set; }


        public BaseCell() { }

        public override void CalculateParameters(double _hExCl, double _hTrCl, double _wOutside, double _dWall, double _yMat)
        {
            dWall = _dWall;
            wOutside = _wOutside;
            wInside = wOutside - 2 * dWall;
            dCover = 1;
            dBottom = 1.5;
            h = Field.dGlobalWater * 0.4;
            a = wOutside * wOutside;
            v = a * h;
            aCover = wInside * wInside;
            aBottom = wInside * wInside;
            aCSWall = a - aCover;
            vMatWall = aCSWall * h;
            vMatCover = aCover * dCover;
            vMatBottom = aBottom * dBottom;
            vMat = vMatWall + vMatCover + vMatBottom;
            vTrCl = 0;
            pMatWall = vMatWall * _yMat;
            pMatCover = vMatCover * _yMat;
            pMatBottom = vMatBottom * _yMat;
            p = pMatWall + pMatCover + pMatBottom + pBallast;
            kp = (pMatWall * h * 0.5 + pMatCover * (h - dCover * 0.5) + pMatBottom * dBottom * 0.5 + pBallast * ( dBottom + dBallast * 0.5)) / p;
        }

        public override double GetLiftingPowerValue(double yWater)
        {
            return v * yWater - p;
        }
    }
}
