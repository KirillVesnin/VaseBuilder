using System;
using Kompas6API5;

namespace Vase
{
    /// <summary>
    ///     Соединитель с КОМПАС-3D
    /// </summary>
    public class KompasConnector
    {
        /// <summary>
        /// Экземпляр КОМПАС-3D
        /// </summary>
        private KompasObject _instance;

        public bool IsActive => _instance != null;

        public void Close()
        {
            _instance?.Quit();
            _instance = null;
        }

        public void Start()
        {
            var type = Type.GetTypeFromProgID("KOMPAS.Application.5");
            _instance = (KompasObject) Activator.CreateInstance(type);

            if (_instance == null)
                throw new NullReferenceException("Не найдена подходящая версия КОМПАС-3D.");

            _instance.Visible = true;
            _instance.ActivateControllerAPI();
        }

        public ksDocument3D CreateDocument3D()
        {
            if (!IsActive)
            throw new NullReferenceException("КОМПАС-3D не запущен.");

            ksDocument3D document3D = _instance.Document3D();
            document3D.Create(false, false);
            return document3D;
        }
    }
}