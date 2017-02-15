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

    class Field
    {
        private static Field instance;

        public double yWater { get; set; } = 1;
        public double dLocalWater { get; set; } = 30;
        public double dGlobalWater { get; set; } = 20;
        public double hWave50 { get; set; } = 6;
        public double hWave001 { get; set; } = 12;
        public double dIce { get; set; } = 3;
        public double durabilityIce { get; set; } = 2;
        public double diameterIce { get; set; } = 3000;
        public double speedIce { get; set; } = 0.5;

        public Field() { }

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
    }

    class CalculationDownStructure
    {
        private static CalculationDownStructure instance;
        private CalculationDownStructure() { }

        public static CalculationDownStructure Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new CalculationDownStructure();
                }
                return instance;
            }
        }  
    }

    public class DownStructure
    {
        public StructureType Type { get; set; }
        public bool isCalculated { get; set; }
        public int countBC { get; set; } = 25;
        public int countSC { get; set; } = 1;
        public double wUpStructure { get; set; } = 100;
        public double pUpStructure { get; set; }
        public double dWallCell { get; set; } = 0.75;
        public double wCell { get; set; } = 20;
        public double hExCl { get; set; }
        public double hTrCl { get; set; }
        public double hStructure { get; set; }
        public double yMat { get; set; } = 2.5;
        public BaseCell baseCell { get; set; } = new BaseCell();
        public Cell supportCell { get; set; } = new Cell();

        public DownStructure(StructureType type)
        {
            switch (type)
            {
                case StructureType.Kesson: countSC = 25; break;
                case StructureType.Monoleg: countSC = 1; break;
                case StructureType.Multileg: countSC = 4; break;
            }
        }

        public DownStructure(int _countBC, int _countSC, float _pUpStructure, float _hStructure, float _yMat)
        {
            isCalculated = true;
            countBC = _countBC;
            countSC = _countSC;
            pUpStructure = _pUpStructure;
            hStructure = _hStructure;
            yMat = _yMat;
        }

        public void CalculateHeightStricture()
        {
            hExCl = 0.5 * Field.Instance.hWave001 + 0.5;
            hStructure = Field.Instance.dGlobalWater + hExCl;
            hTrCl = 1;
        }

        public double CalculateInertiaMoment()
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

        public double CalculateArea()
        {
            return (Convert.ToDouble(countBC) * baseCell.v + 
                    Convert.ToDouble(countSC) * supportCell.v - 
                    Convert.ToDouble(countSC) * Math.Pow(supportCell.wOutside, 2) * hTrCl);
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

        public bool FloatingStabilityCalculate()
        {

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
        public double vTrCl { get; set; }
        public double wInside { get; set; }
        public double wOutside { get; set; }
        public double aCSWall { get; set; }
        public double vMatWall { get; set; }
        public double pMatWall { get; set; }

        public virtual void CalculateParameters(double _hStructure, double _hTrCl, double _wOutside, double _dWall, double _yMat)
        {
            dWall = _dWall;
            h = _hStructure - Field.Instance.dGlobalWater * 0.4;
            dWall = _dWall;
            wOutside = _wOutside;
            wInside = wOutside - 2 * dWall;
            a = wOutside * wOutside;
            aCSWall = a - wInside * wInside;
            vMatWall = aCSWall * h;
            v = a * (h - _hTrCl);
            vMat = vMatWall;
            vTrCl = a * _hTrCl;
            pMatWall = vMatWall * _yMat;
            p = pMatWall;
            kp = Field.Instance.dGlobalWater * 0.4 + h * 0.5;
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

        public BaseCell() { }

        public override void CalculateParameters(double _hStructure, double _hTrCl, double _wOutside, double _dWall, double _yMat)
        {
            dWall = _dWall;
            wOutside = _wOutside;
            wInside = wOutside - 2 * dWall;
            dCover = (dWall / 3) * 4;
            dBottom = 2 * dCover;
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
            vTrCl = a * _hTrCl;
            pMatWall = vMatWall * _yMat;
            pMatCover = vMatCover * _yMat;
            pMatBottom = vMatBottom * _yMat;
            p = pMatWall + pMatCover + pMatBottom;
            kp = (pMatWall * h * 0.5 + pMatCover * (h - dCover * 0.5) + pMatBottom * dBottom * 0.5) / p;
        }
    }
}
