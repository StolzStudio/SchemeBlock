using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace tryhard
{
    public class MetaObjectInfo
    {
        public string Name { get; set; }
        public List<string> PossibleLink { get; set; }
        public List<string> Properties { get; set; }
    }

    public class Project
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int SelectedCell { get; set; } = -1;
        public int EstimatedFieldId { get; set; }
        public FieldSlice FieldParameters { get; set; }
        public ObjectsStructure Structure { get; set; } = new ObjectsStructure();
        public Dictionary<int, int> SelectedStructureTypes { get; set; } = new Dictionary<int, int>();
        public Dictionary<int, List<DownStructure>> DownStructures { get; set; } = new Dictionary<int, List<DownStructure>>();

        public Project() { }
        public Project(Project originalProject)
        {
            Id = originalProject.Id;
            Name = originalProject.Name;
            SelectedCell = originalProject.SelectedCell;
            EstimatedFieldId = originalProject.EstimatedFieldId;
            Structure = new ObjectsStructure(originalProject.Structure);
            SelectedStructureTypes = new Dictionary<int, int>();
            FieldParameters = new FieldSlice();
            FieldParameters.Fill();
            foreach (int key in originalProject.SelectedStructureTypes.Keys)
                SelectedStructureTypes.Add(key, originalProject.SelectedStructureTypes[key]);
            foreach (int key in originalProject.DownStructures.Keys)
            {
                List<DownStructure> downStructures = new List<DownStructure>();
                foreach (DownStructure downStructure in originalProject.DownStructures[key])
                    downStructures.Add(new DownStructure(downStructure));
                DownStructures.Add(key, downStructures);
            }
        }
    }

    public class ObjectsStructure
    {
        public List<StructuralObject> Objects { get; set; } = new List<StructuralObject>();
        public List<LinkStructuralObject> Links { get; set; } = new List<LinkStructuralObject>();

        public ObjectsStructure() { }
        public ObjectsStructure(ObjectsStructure originalStructure)
        {
            Objects = originalStructure.Objects.Select(s => new StructuralObject(s)).ToList();
            Links = originalStructure.Links.Select(l => new LinkStructuralObject(l)).ToList();
        }
    }

    public class LinkStructuralObject
    {
        public int FirstBlockIndex { get; set; }
        public int SecondBlockIndex { get; set; }
        public string LinkParameter { get; set; }

        public LinkStructuralObject() { }
        public LinkStructuralObject(LinkStructuralObject originalLink)
        {
            FirstBlockIndex = originalLink.FirstBlockIndex;
            SecondBlockIndex = originalLink.SecondBlockIndex;
            LinkParameter = originalLink.LinkParameter;
        }
    }

    public class StructuralObject
    {
        public int Id { get; set; }
        public int Index { get; set; }
        public string Type { get; set; }
        public Point Coordinates { get; set; } = new Point();

        public StructuralObject() { }
        public StructuralObject(StructuralObject originObject)
        {
            Id = originObject.Id;
            Index = originObject.Index;
            Type = originObject.Type;
            Coordinates = new Point(originObject.Coordinates.X, originObject.Coordinates.Y);
        }
    }

    public abstract class BaseObject
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class MaterialObject : BaseObject
    {
        public long Weight { get; set; }
        public long Volume { get; set; }
        public long Cost { get; set; }
        public ObjectsStructure Structure { get; set; }
    }

    public class FieldParameters : BaseObject
    {
        public long FluidOutput { get; set; }
        public int HolesAmount { get; set; }
        public int OilQualityId { get; set; }
        public long WaterInput { get; set; }
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
        public long Input { get; set; }
        public long Output { get; set; }
    }

    public class Pipe : MaterialObject
    {
        public int Diameter { get; set; }
        public string Material { get; set; }
        public long Length { get; set; }
    }

    public class ComplicatedObject : MaterialObject
    {
        public int PeopleDemand { get; set; }
        public long ElectricityDemand { get; set; }
    }

    public class Nnpv : ComplicatedObject
    {
        public long WaterInput { get; set; }
        public long WaterOutput { get; set; }
    }

    public class Fu : ComplicatedObject
    {
        public long GasInput { get; set; }
    }

    public class Rpv : ComplicatedObject
    {
        public long WaterInput { get; set; }
        public long WaterOutput { get; set; }
        public long WaterVolume { get; set; }
    }

    public class Upn : ComplicatedObject
    {
        public long FluidInput { get; set; }
        public long OilOutput { get; set; }
        public long WetGasOutput { get; set; }
        public long WaterOutput { get; set; }
    }

    public class Ukppv : ComplicatedObject
    {
        public long WaterInput { get; set; }
        public long WaterOutput { get; set; }
    }

    public class Ukpg : ComplicatedObject
    {
        public long WetGasInput { get; set; }
        public long GasOutput { get; set; }
    }

    public class Rtn : ComplicatedObject
    {
        public long OilInput { get; set; }
        public long OilOutput { get; set; }
        public long OilVolume { get; set; }
    }

    public class Dks : ComplicatedObject
    {
        public int PressureGradient { get; set; }
        public long GasInput { get; set; }
        public long GasOutput { get; set; }
    }

    public class Rr : ComplicatedObject
    {
        public long MaxReagentInput { get; set; }
    }

    public class Dk : ComplicatedObject
    {
        public int HolesAmount { get; set; }
        public long FluidInput { get; set; }
        public long FluidOutput { get; set; }
        public long WaterInput { get; set; }
        public long ReagentInput { get; set; }
    }

    public class Complex: ComplicatedObject
    {
        public int EstimatedFieldId { get; set; }
    }

    public class MiningComplex: Complex
    {
        public long FluidOutput { get; set; }
    }

    public class ProcessingComplex : Complex
    {
        public long FluidInput { get; set; }
        public long OilOutput { get; set; }
        public long GasOutput { get; set; }
    }

    public class IntegratedComplex : Complex
    {
        public long FluidInput { get; set; }
        public long FluidOutput { get; set; }
        public long OilOutput { get; set; }
        public long GasOutput { get; set; }
    }
}
