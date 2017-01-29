using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tryhard
{
    public class MetaObjectInfo
    {
        public string Name { get; set; }
        public List<string> PossibleLink { get; set; }
        public List<string> Properties { get; set; }
    }

    public abstract class BaseObject
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class MaterialObject : BaseObject
    {
        public int Weight { get; set; }
        public int Volume { get; set; }
        public int Cost { get; set; }
    }

    public class FieldParameters : BaseObject
    {
        public int FluidOutput { get; set; }
        public int HolesAmount { get; set; }
        public int OilQualityId { get; set; }
        public int WaterInput { get; set; }
        public int ReserveDaysRvp { get; set; }
        public int ReserveDaysRtn { get; set; }
    }

    public class OilQuality : BaseObject
    {
        public float OilProportion { get; set; }
        public float GasProportion { get; set; }
        public float WaterProportion { get; set; }
    }

    public class Bolt : MaterialObject
    {
        public int Diameter { get; set; }
        public string Material { get; set; }
    }

    public class Pump : MaterialObject
    {
        public int Input { get; set; }
        public int Output { get; set; }
    }

    public class Pipe : MaterialObject
    {
        public int Diameter { get; set; }
        public string Material { get; set; }
        public int Length { get; set; }
    }

    public class ComplicatedObject : MaterialObject
    {
        public int PeopleDemand { get; set; }
        public int ElectricityDemand { get; set; }
    }

    public class Nnpv : ComplicatedObject
    {
        public int WaterInput { get; set; }
        public int WaterOutput { get; set; }
    }

    public class Fu : ComplicatedObject
    {
        public int GasInput { get; set; }
    }

    public class Rpv : ComplicatedObject
    {
        public int WaterInput { get; set; }
        public int WaterOutput { get; set; }
        public int WaterVolume { get; set; }
    }

    public class Upn : ComplicatedObject
    {
        public int FluidInput { get; set; }
        public int OilOutput { get; set; }
        public int WetGasOutput { get; set; }
        public int WaterOutput { get; set; }
    }

    public class Ukppv : ComplicatedObject
    {
        public int WaterInput { get; set; }
        public int WaterOutput { get; set; }
    }

    public class Ukpg : ComplicatedObject
    {
        public int WetGasInput { get; set; }
        public int GasOutput { get; set; }
    }

    public class Rtn : ComplicatedObject
    {
        public int OilInput { get; set; }
        public int OilOutput { get; set; }
        public int OilVolume { get; set; }
    }

    public class Dks : ComplicatedObject
    {
        public int PressureGradient { get; set; }
        public int GasInput { get; set; }
        public int GasOutput { get; set; }
    }

    public class Rr : ComplicatedObject
    {
        public int MaxReagentInput { get; set; }
    }

    public class Dk : ComplicatedObject
    {
        public int HolesAmount { get; set; }
        public int FluidInput { get; set; }
        public int FluidOutput { get; set; }
        public int WaterInput { get; set; }
        public int ReagentInput { get; set; }
    }
}
