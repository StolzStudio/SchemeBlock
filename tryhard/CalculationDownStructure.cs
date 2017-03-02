using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tryhard
{
    public enum StructureType { Kesson = 0, Monoleg = 1, Multileg = 2 };

    public class FieldSlice
    {
        public double yWater { get; set; } = 1;
        public double dLocalWater { get; set; } = 30;
        public double dGlobalWater { get; set; } = 50;
        public double hWave50 { get; set; } = 6;
        public double hWave001 { get; set; } = 12;
        public double dIce { get; set; } = 3;
        public double durabilityIce { get; set; } = 2;
        public double diameterIce { get; set; } = 3000;
        public double speedIce { get; set; } = 0.5;
        public double durabilityGround { get; set; } = 250;

        public FieldSlice() { }

        public void Fill()
        {
            yWater = Field.Instance.yWater;
            dLocalWater = Field.Instance.dLocalWater;
            dGlobalWater = Field.Instance.dGlobalWater;
            hWave50 = Field.Instance.hWave50;
            hWave001 = Field.Instance.hWave001;
            dIce = Field.Instance.dIce;
            durabilityIce = Field.Instance.durabilityIce;
            diameterIce = Field.Instance.diameterIce;
            speedIce = Field.Instance.speedIce;
            durabilityGround= Field.Instance.durabilityGround;
        }
    }

    public class Field
    {
        public double yWater { get; set; } = 1;
        public double dLocalWater { get; set; } = 30;
        public double dGlobalWater { get; set; } = 50;
        public double hWave50 { get; set; } = 6;
        public double hWave001 { get; set; } = 12;
        public double dIce { get; set; } = 3;
        public double durabilityIce { get; set; } = 2;
        public double diameterIce { get; set; } = 3000;
        public double speedIce { get; set; } = 0.5;
        public double durabilityGround { get; set; } = 250;

        private static Field instance;
        private Field () { }
        public static Field Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Field();
                }
                return instance;
            }
        }

        public void Initialize(Field originalField)
        {
            yWater = originalField.yWater;
            dLocalWater = originalField.dLocalWater;
            dGlobalWater = originalField.dGlobalWater;
            hWave50 = originalField.hWave50;
            hWave001 = originalField.hWave001;
            dIce = originalField.dIce;
            durabilityIce = originalField.durabilityIce;
            diameterIce = originalField.diameterIce;
            speedIce = originalField.speedIce;
            durabilityGround = originalField.durabilityGround;
        }

        public void Initialize(FieldSlice originalField)
        {
            yWater = originalField.yWater;
            dLocalWater = originalField.dLocalWater;
            dGlobalWater = originalField.dGlobalWater;
            hWave50 = originalField.hWave50;
            hWave001 = originalField.hWave001;
            dIce = originalField.dIce;
            durabilityIce = originalField.durabilityIce;
            diameterIce = originalField.diameterIce;
            speedIce = originalField.speedIce;
            durabilityGround = originalField.durabilityGround;
        }

        public void Initialize()
        {
            yWater = 1;
            dLocalWater = 30;
            dGlobalWater = 50;
            hWave50 = 6;
            hWave001 = 12;
            dIce = 3;
            durabilityIce = 2;
            diameterIce = 3000;
            speedIce = 0.5;
            durabilityGround = 2500;
        }
    }

    public class DownStructure
    {
        public bool isCalculated { get; set; }
        public StructureType type { get; set; }
        public int countBC { get; set; }
        public int countSC { get; set; }
        public double wUpStructure { get; set; }
        public double pUpStructure { get; set; }
        public double dWallCell { get; set; }
        public double hStructure { get; set; }
        public double wCell { get; set; }
        public double hExCl { get; set; }
        public double hTrCl { get; set; }
        public double yMat { get; set; }
        public double yMatBallast { get; set; }
        public Cell supportCell { get; set; }
        public BaseCell baseCell { get; set; }
        public double minTrCl { get; set; }
        public double freeVolume { get; set; }
        public double weight { get; set; }
        public double cost { get; set; }

        public DownStructure() { }
        public DownStructure(DownStructure originalStructure)
        {
            isCalculated = originalStructure.isCalculated;
            type = originalStructure.type;
            countBC = originalStructure.countBC;
            countSC = originalStructure.countSC;
            wUpStructure = originalStructure.wUpStructure;
            pUpStructure = originalStructure.pUpStructure;
            dWallCell = originalStructure.dWallCell;
            hStructure = originalStructure.hStructure;
            wCell = originalStructure.wCell;
            hExCl = originalStructure.hExCl;
            hTrCl = originalStructure.hTrCl;
            yMat = originalStructure.yMat;
            yMatBallast = originalStructure.yMatBallast;
            supportCell = new Cell(originalStructure.supportCell);
            baseCell = new BaseCell(originalStructure.baseCell);
            cost = originalStructure.cost;
            minTrCl = originalStructure.minTrCl;
        }


        public DownStructure(StructureType type)
        {
            SetCellCount(type);
            DefaultInitialize();
        }

        public DownStructure(StructureType type, Int64 weight) : this(type)
        {
            CalculateDownStructure(weight);
        }

        private void SetCellCount(StructureType type)
        {
            this.type = type;
            switch (this.type)
            {
                case StructureType.Kesson: countSC = 25; countBC = 25; break;
                case StructureType.Monoleg: countSC = 1; countBC = 9; break;
                case StructureType.Multileg: countSC = 4; countBC = 25; break;
            }
        }

        public void DefaultInitialize()
        {
            minTrCl = 0.5 * Field.Instance.hWave001 + 2.5;
            wUpStructure = 100;
            dWallCell = 0.75;
            wCell = 20;
            yMat = 2.5;
            yMatBallast = 1.6;
        }

        public bool CalculateDownStructure(double _pUpStructure)
        {
            SetCellCount(type);
            supportCell = new Cell();
            baseCell = new BaseCell();
            weight = cost = 0;
            pUpStructure = _pUpStructure;
            if (!isStability()) return false;
            weight = countSC * supportCell.p + countBC * baseCell.p;
            freeVolume = countBC * baseCell.a * (baseCell.h - baseCell.dCover - baseCell.dBottom - baseCell.dBallast);
            cost = weight * 400000.0 + freeVolume * 0.85 * 100000;
            return true;
        }

        public bool isStability()
        {
            bool isStability = false;
            while (!isStability)
            {
                if (CalculateBaseParameters())
                    if (CalculateEhouthBallast())
                        if (CheckGroundDurability() && CheckFlatShift())
                            isStability = true;
                if (!isStability && !AddBaseCell())
                    return false;
            }
            return true;
        }

        public void CalculateHeightStricture()
        {
            hExCl = 0.5 * Field.Instance.hWave001 + 0.5;
            hStructure = Field.Instance.dGlobalWater + hExCl;
            hTrCl = hStructure;
        }

        public bool CalculateBaseParameters()
        {
            CalculateHeightStricture();
            supportCell.CalculateParameters(hExCl, hStructure - Field.Instance.dGlobalWater * 0.4, wCell, dWallCell, yMat);
            baseCell.CalculateParameters(hExCl, Field.Instance.dGlobalWater * 0.4, wCell, dWallCell, yMat);
            double vMatStr = countBC * baseCell.vMat + countSC * supportCell.vMat;
            double vStr = countBC * baseCell.v + countSC * supportCell.v;
            double v0 = vMatStr * yMat / Field.Instance.yWater;
            double w = (vStr - v0) / v0;
            double l = w * vStr;
            if (v0 <= countBC * baseCell.v)
            {
                double cl = baseCell.h - v0 / (countBC * baseCell.a);
                baseCell.vTrCl = cl * baseCell.a;
                supportCell.vTrCl = supportCell.a * supportCell.h;
                hTrCl = cl + supportCell.h;
            }
            else
            {
                double cl = supportCell.h - (v0 - countBC * baseCell.a) / (countSC * supportCell.a);
                baseCell.vTrCl = 0;
                supportCell.vTrCl = cl * supportCell.a;
                hTrCl = cl;
                return cl >= hExCl + 2;
            }
            return true;
        }

        public bool CalculateEhouthBallast()
        {
            while (GetLiftingPower() <= 0)
            {
                if (ChangeTrCl())
                {
                    supportCell.CalculateParameters(hExCl, hTrCl, wCell, dWallCell, yMat);
                    if (!baseCell.FillBallast(countBC, GetLiftingPower(), yMatBallast)) return false;
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
                        if (!baseCell.FillBallast(countBC, GetLiftingPower(), yMatBallast)) return false;
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
        //
        //Проверка на плоский сдвиг
        //
        public bool CheckFlatShift()
        {
            return (CalculateLongitudinalForce(Field.Instance.yWater) * Math.Tan(180 / Math.PI * 20)) / CalculateIceLoad() > 1;
        }
        //
        //Проверка прочности по грунту
        //
        public bool CheckGroundDurability()
        {
            double longitudinalForce = CalculateLongitudinalForce(Field.Instance.yWater);
            double aBaseCells = countBC * baseCell.a;
            double armPower = CalculateArmPower();
            double iceLoad = CalculateIceLoad();
            double momentFlexResistance = CalculateMomentFlexResistance();
            double sigma = longitudinalForce / aBaseCells - (armPower * iceLoad) / momentFlexResistance;
            return Field.Instance.durabilityGround / Math.Abs(sigma) > 1;
        }
        //
        //Рассчет ледовой нагрузки
        //
        public double CalculateIceLoad()
        {
            double dh = supportCell.wOutside / Field.Instance.dIce;
            double kb = 0;
            double kv = 0;
            double Fcp = 0;
            double Fbp = 0;
            CalculateKbKv(out kb, out kv, dh, Field.Instance.speedIce, Field.Instance.diameterIce);
            Fbp = 0.83 * kb * kv * Field.Instance.durabilityIce * Field.Instance.diameterIce * Field.Instance.dIce;
            Fcp = 0.00126 * Field.Instance.speedIce * Field.Instance.dIce * Math.Sqrt(Fbp * supportCell.a);
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
            return Field.Instance.dGlobalWater - (17.0 / 30.0) * Field.Instance.dIce;
        }

        //
        //Вычисление избыточной плавучести
        //
        public double GetLiftingPower()
        {
            return countSC * supportCell.GetLiftingPowerValue(Field.Instance.yWater) + 
                   countBC * baseCell.GetLiftingPowerValue(Field.Instance.yWater);
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
            double numerator = baseCell.hTrCl > 0 ? Math.Pow(countBC * baseCell.wOutside, 4) : 
                                                    Math.Pow(countSC * supportCell.wOutside, 4);
            return numerator / 12d;
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
            if (type == StructureType.Kesson || type == StructureType.Multileg)
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
            if (countBC >= 121) return false;
            countBC = Convert.ToInt32(Math.Pow(Math.Sqrt(countBC) + 2, 2));
            baseCell.FreeBallast();
            hTrCl = 0;
            return true;
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
        public double hTrCl { get; set; }
        public double vTrCl { get; set; }
        public double wInside { get; set; }
        public double wOutside { get; set; }
        public double aCSWall { get; set; }
        public double vMatWall { get; set; }
        public double pMatWall { get; set; }

        public Cell() { }
        public Cell(Cell originCell)
        {
            a = originCell.a;
            h = originCell.h;
            v = originCell.v;
            p = originCell.p;
            kp = originCell.kp;
            vMat = originCell.vMat;
            dWall = originCell.dWall;
            hTrCl = originCell.hTrCl;
            vTrCl = originCell.vTrCl;
            wInside = originCell.wInside;
            wOutside = originCell.wOutside;
            aCSWall = originCell.aCSWall;
            vMatWall = originCell.vMatWall;
            pMatWall = originCell.pMatWall;
        }

        public virtual void CalculateParameters(double _hExCl, double _hTrCl, double _wOutside, double _dWall, double _yMat)
        {
            dWall = _dWall;
            h = _hExCl + Field.Instance.dGlobalWater * 0.6;
            wOutside = _wOutside;
            wInside = wOutside - 2 * dWall;
            a = wOutside * wOutside;
            aCSWall = a - wInside * wInside;
            vMatWall = aCSWall * h;
            v = a * h;
            vMat = vMatWall;
            pMatWall = vMatWall * _yMat;
            p = pMatWall;
            CalculateKP();
            CalculateTrCl(_hTrCl);
        }

        public virtual void CalculateTrCl(double _hTrCl)
        {
            hTrCl = _hTrCl;
            vTrCl = a * hTrCl;
        }

        public virtual void CalculateKP()
        {
            kp = Field.Instance.dGlobalWater * 0.4 + h * 0.5;
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
        public BaseCell(BaseCell originalCell) : base()
        {
            dCover = originalCell.dCover;
            aCover = originalCell.aCover;
            vMatCover = originalCell.vMatCover;
            pMatCover = originalCell.pMatCover;
            dBottom = originalCell.dBottom;
            aBottom = originalCell.aBottom;
            vMatBottom = originalCell.vMatBottom;
            pMatBottom = originalCell.pMatBottom;
            dBallast = originalCell.dBallast;
            pBallast = originalCell.pBallast;
        }

        public override void CalculateParameters(double _hExCl, double _hTrCl, double _wOutside, double _dWall, double _yMat)
        {
            dWall = _dWall;
            wOutside = _wOutside;
            wInside = wOutside - 2 * dWall;
            if (Field.Instance.dGlobalWater < 25)
            {
                dCover = 0.5;
                dBottom = 1.0;
            } else
            {
                dCover = 1;
                dBottom = 1.5;
            }
            h = Field.Instance.dGlobalWater * 0.4;
            a = wOutside * wOutside;
            v = a * h;
            aCover = wInside * wInside;
            aBottom = wInside * wInside;
            aCSWall = a - aCover;
            vMatWall = aCSWall * h;
            vMatCover = aCover * dCover;
            vMatBottom = aBottom * dBottom;
            vMat = vMatWall + vMatCover + vMatBottom;
            pMatWall = vMatWall * _yMat;
            pMatCover = vMatCover * _yMat;
            pMatBottom = vMatBottom * _yMat;
            p = pMatWall + pMatCover + pMatBottom + pBallast;
            CalculateKP();
            CalculateTrCl(_hTrCl);
        }

        public void FreeBallast()
        {
            pBallast = 0;
            dBallast = 0;
        }

        public bool FillBallast(int countBC, double liftingPower, double yMatBallast)
        {
            pBallast = liftingPower / Convert.ToDouble(countBC);
            dBallast = pBallast / (aBottom * yMatBallast);
            if (dBallast > h - dCover - dBottom) return false;
            return true;
        }

        public override void CalculateKP()
        {
            kp = (pMatWall * h * 0.5 + pMatCover * (h - dCover * 0.5) + pMatBottom * dBottom * 0.5 + pBallast * (dBottom + dBallast * 0.5)) / p;
        }

        public override double GetLiftingPowerValue(double yWater)
        {
            return v * yWater - p;
        }
    }
}
