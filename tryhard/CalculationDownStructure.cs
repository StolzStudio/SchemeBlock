using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tryhard
{
    public enum StructureType { Kesson = 0, Monoleg = 1, Multileg = 2 };

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
        public float wUpStructure { get; set; } = 100;
        public float pUpStructure { get; set; }
        public float dLocalWater { get; set; } = 30;
        public float dGlobalWater { get; set; } = 20;
        public float dWallCell { get; set; } = (float)0.75;
        public float wCell { get; set; } = 20;
        public float hWave001 { get; set; } = 12;
        public float hWave50 { get; set; } = 6;
        public float hExCl { get; set; }
        public float hTrCl { get; set; }
        public float hStructure { get; set; }
        public float yMat { get; set; } = (float)2.5;
        public float yWater { get; set; } = 1;

        public BaseCell baseCell = new BaseCell();
        public Cell supportCell = new Cell();

        public DownStructure(StructureType type)
        {
            switch (type)
            {
                case StructureType.Kesson: countSC = 25; break;
                case StructureType.Monoleg: countSC = 1; break;
                case StructureType.Multileg: countSC = 4; break;
            }
        }

        public DownStructure(int _countBC, int _countSC, float _pUpStructure, float _dGlobalWater, 
                             float _hWave001, float _hWave50, float _hStructure, float _yMat, float _yWater)
        {
            isCalculated = true;
            countBC = _countBC;
            countSC = _countSC;
            pUpStructure = _pUpStructure;
            dGlobalWater = _dGlobalWater;
            hWave001 = _hWave001;
            hWave50 = _hWave50;
            hStructure = _hStructure;
            yMat = _yMat;
            yWater = _yWater;
        }

        public void CalculateHeightStricture()
        {
            hExCl = (float)0.5 * hWave001 + (float)0.5;
            hStructure = dGlobalWater + hExCl;
            hTrCl = 1;
        }
    }

    public class Cell
    {
        public float a { get; set; }
        public float h { get; set; }
        public float v { get; set; }
        public float p { get; set; }
        public float kp { get; set; }
        public float vMat { get; set; }
        public float dWall { get; set; }
        public float vTrCl { get; set; }
        public float wInside { get; set; }
        public float wOutside { get; set; }
        public float aCSWall { get; set; }
        public float vMatWall { get; set; }
        public float pMatWall { get; set; }

        public virtual void CalculateParameters(float _dGlobalWater, float _hStructure, float _hTrCl, float _wOutside, float _dWall, float _yMat)
        {
            dWall = _dWall;
            h = _hStructure - _dGlobalWater * (float)0.4;
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
            kp = _dGlobalWater * (float)0.4 + h * (float)0.5;
        }
    }

    public class BaseCell : Cell
    {
        public float dCover { get; set; }
        public float aCover { get; set; }
        public float vMatCover { get; set; }
        public float pMatCover { get; set; }

        public float dBottom { get; set; }
        public float aBottom { get; set; }
        public float vMatBottom { get; set; }
        public float pMatBottom { get; set; }

        public BaseCell() { }

        public override void CalculateParameters(float _dGlobalWater, float _hStructure, float _hTrCl, float _wOutside, float _dWall, float _yMat)
        {
            dWall = _dWall;
            wOutside = _wOutside;
            wInside = wOutside - 2 * dWall;
            dCover = (dWall / 3) * 4;
            dBottom = 2 * dCover;
            h = _dGlobalWater * (float)0.4;
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
            kp = (pMatWall * h * (float)0.5 + pMatCover * (h - dCover * (float)0.5) + pMatBottom * dBottom * (float)0.5) / p;
        }
    }
}
