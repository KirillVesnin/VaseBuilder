using Kompas6API5;
using Kompas6Constants3D;

namespace Vase
{
    public class VaseBuilder
    {
        
        private readonly VaseParameters _vaseParameters;

        private readonly ksPart _part;

        public VaseBuilder(ksDocument3D documents3D, VaseParameters vaseParameters)
        {
            _part = documents3D.GetPart(pTop_part);
            _vaseParameters = vaseParameters;
        }
        /// <summary> 
        /// Создает линию. 
        /// </summary> 
        /// <param name="document2D">2D документ.</param> 
        /// <param name="xStart">Начальный x.</param> 
        /// <param name="yStart">Начальный y.</param> 
        /// <param name="xEnd">Конечный x.</param> 
        /// <param name="yEnd">Конечный y.</param> 
        private static void CreateLine(ksDocument2D document2D, double xStart, double yStart, double xEnd, double yEnd)
        {
            document2D.ksLineSeg(xStart, yStart, xEnd, yEnd, 1);
        }
        /// <summary> 
        /// Создает кинематическую операцию. 
        /// </summary> 
        /// <param name="sketch">Эскиз для операции.</param> 
        /// <param name="path">Путь операции.</param> 
        private void CreateBaseEvolution(ksEntity sketch, ksEntity path)
        {
            ksEntity entityCutEvolution = _part.NewEntity(o3d_baseEvolution);
            ksBaseEvolutionDefinition cutEvolutionDefinition = entityCutEvolution.GetDefinition();

            cutEvolutionDefinition.sketchShiftType = 1;

            cutEvolutionDefinition.SetSketch(sketch);

            ksEntityCollection pathParts = cutEvolutionDefinition.PathPartArray();
            pathParts.Clear();
            pathParts.Add(path);
            entityCutEvolution.Create();
        }
        /// <summary> 
        /// Получает смещенную плоскость. 
        /// </summary> 
        /// <param name="basePlane">Базовая плоскость.</param> 
        /// <param name="offset">Смещение.</param> 
        /// <param name="direction">Направление смещения.</param> 
        /// <returns>Смещенная плоскость.</returns> 
        private ksEntity CreatePlaneOffset(ksEntity basePlane, double offset, bool direction = true)
        {
            ksEntity planeOffset = _part.NewEntity(o3d_planeOffset);
            var planeOffsetDefinition = planeOffset.GetDefinition();

            planeOffsetDefinition.direction = direction;
            planeOffsetDefinition.offset = offset;

            planeOffsetDefinition.SetPlane(basePlane);

            planeOffset.Create();
            return planeOffset;
        }

        /// <summary> 
        /// Создает пустой эскиз. 
        /// </summary> 
        /// <param name="plane">Плоскость.</param> 
        /// <returns>Эскиз.</returns> 
        private ksEntity CreateSketch(ksEntity plane)
        {
            var sketch =

            _part.NewEntity(o3d_sketch);
            ksSketchDefinition sketchDefinition = sketch.GetDefinition();

            sketchDefinition.SetPlane(plane);

            sketch.Create();

            return sketch;
        }

        /// <summary> 
        /// Создает эскиз с треугольником. 
        /// </summary> 
        /// <param name="plane">Плоскость.</param> 
        /// <param name="x1">x1.</param> 
        /// <param name="y1">y1.</param> 
        /// <param name="x2">x2.</param> 
        /// <param name="y2">y2.</param> 
        /// <param name="x3">x3.</param> 
        /// <param name="y3">y3.</param> 
        /// <returns>Эскиз.</returns> 
        private ksEntity CreateTriangleSketch(ksEntity plane,
        double x1, double y1,
        double x2, double y2,
        double x3, double y3)
        {
            var sketch = CreateSketch(plane);
            ksSketchDefinition sketchDefinition = sketch.GetDefinition();

            ksDocument2D document2D = sketchDefinition.BeginEdit();

            CreateLine(document2D, x1, y1, x2, y2);
            CreateLine(document2D, x2, y2, x3, y3);
            CreateLine(document2D, x3, y3, x1, y1);

            sketchDefinition.EndEdit();

            return sketch;
        }

       
        /// <summary> 
        /// Получает поверхность по координатам. 
        /// </summary> 
        /// <param name="x">Координата x.</param> 
        /// <param name="y">Координата y.</param> 
        /// <param name="z">Координата z.</param> 
        /// <returns>Поверхность.</returns> 
        private ksEntity GetFaceByPoint(double x, double y, double z)
        {
            ksEntityCollection faceCollection = _part.EntityCollection(o3d_face);
            faceCollection.SelectByPoint(x, y, z);
            return faceCollection.First();
        }

        /// <summary> 
        /// Устанавливает плоскости. 
        /// </summary> 
        private void SetPlanes()
        {
            _bottomPlane = _part.GetDefaultEntity(o3d_planeXoy);
            _threadSketchPlane = _part.GetDefaultEntity(o3d_planeXoz);
            _topPlane = CreatePlaneOffset(_bottomPlane, _bottleParameters.LengthFullBottle);
            _baseTopPlane = CreatePlaneOffset(_bottomPlane, _bottleParameters.BaseLength);
            _capBottomPlane = CreatePlaneOffset(_topPlane, CapHeight, false);
            _threadTopPlane = CreatePlaneOffset(_topPlane, ThreadIndent, false);
        }

        #region Origins 

        /// <summary> 
        /// Начало координат оси X. 
        /// </summary> 
        private const double OriginX = 0;

        /// <summary> 
        /// Начало координат оси Y. 
        /// </summary> 
        private const double

        OriginY = 0;

        /// <summary> 
        /// Начало координат оси Z. 
        /// </summary> 
        private const double OriginZ = 0;

        #endregion

        #region ConstSizes 

        /// <summary> 
        /// Радиус скругления основания. 
        /// </summary> 
        private const double FilletBaseRadius = 5;

        /// <summary> 
        /// Углубление в горлышке для крышки. 
        /// </summary> 
        private const double BottleneckRecessForCap = 1;

        /// <summary> 
        /// Высота резьбы. 
        /// </summary> 
        private const double ThreadHeight = CapHeight - ThreadIndent;

        /// <summary> 
        /// Высота крышки. 
        /// </summary> 
        private const double CapHeight = 5;

        /// <summary> 
        /// Отступ для резьбы. 
        /// </summary> 
        private const double ThreadIndent = 0.5;

        #endregion

        #region AuxiliarySizes 

        /// <summary> 
        /// Радиус основания. 
        /// </summary> 
        private readonly double _baseRadius;

        /// <summary> 
        /// Радиус горлышка для крышки. 
        /// </summary> 
        private readonly double _bottleneckForCapRadius;

        /// <summary> 
        /// Радиус горлышка. 
        /// </summary> 
        private readonly double _bottleneckRadius;

        #endregion

        #region Planes 

        /// <summary> 
        /// Плоскость верха основания бутылки. 
        /// </summary> 
        private ksEntity _baseTopPlane;

        /// <summary> 
        /// Плоскость низа бутылки. 
        /// </summary> 
        private ksEntity _bottomPlane;

        /// <summary> 
        /// Плоскость низа крышки. 
        /// </summary> 
        private ksEntity _capBottomPlane;

        /// <summary> 
        /// Плоскость для эскиза резьбы. 
        /// </summary> 
        private ksEntity _threadSketchPlane;

        /// <summary> 
        /// Плоскость верха резьбы. 
        /// </summary> 
        private ksEntity _threadTopPlane;

        /// <summary> 
        /// Плоскость верха бутылки. 
        /// </summary> 
        private ksEntity _topPlane;

        #endregion

        #region KompasConstants 

        /// <summary> 
        /// Главный компонент, в составе которо го находится новый или редактируемый или указанный компонент. 
        /// </summary> 
        private const short pTop_part = -1;

        /// <summary> 
        /// Эскиз. 
        /// </summary> 
        private const short o3d_sketch = 5;

        /// <summary> 
        /// Базовая операция выдавливания. 
        /// </summary> 
        private const short o3d_baseExtrusion = 24;

        /// <summary> 
        /// Вырезать выдавливанием. 
        /// </summary> 
        private const short o3d_cutExtrusion = 26;

        /// <summary> 
        /// Кинематическая операция. 
        /// </summary> 
        private const short o3d_baseEvolution = 45;

        /// <summary> 
        /// Коническая спираль. 
        /// </summary> 
        private const short o3d_cylindricSpiral = 56;

        /// <summary> 
        /// Поверхность. 
        /// </summary> 
        private const short o3d_face = (short) Obj3dType.o3d_face;

        /// <summary> 
        /// Плоскость XOY. 
        /// </summary>
      /// 
        private const short o3d_planeXoy = (short) Obj3dType.o3d_planeXOY;

        /// <summary> 
        /// Плоскость XOZ. 
        /// </summary> 
        private const short o3d_planeXoz = 2;

        /// <summary> 
        /// Смещённая плоскость. 
        /// </summary> 
        private const short o3d_planeOffset = 14;

     #endregion
    }
}
