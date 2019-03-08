using System;
using NUnit.Framework;
using Vase;
using Assert = NUnit.Framework.Assert;

namespace VaseTest
{
    [TestFixture]
    public class KompasConnectorTests
    {
        [Test]
        [TestCase(TestName = "Проверка дизактивации КОМПАС-3D.")]
        //[Ignore("Совместимая версия не установлена. Tест не выполнится.")] 
        public void Close()
        {
            var kompasConnector = new KompasConnector();

            kompasConnector.Start();

            Assert.DoesNotThrow(() => kompasConnector.Close());
        }

        [Test]
        [TestCase(TestName = "Проверка создания 3D документа, если компас был запущен.")]
        //[Ignore("Совместимая версия не установлена. Tест не выполнится.")] 
        public void CreateDocument_KompasIsNotStarted()
        {
            var kompasConnector = new KompasConnector();

            Assert.Throws<NullReferenceException>(() => kompasConnector.CreateDocument3D());
        }

        [Test]
        [TestCase(TestName = "Проверка создания 3D документа, если компас был запущен.")]
        //[Ignore("Совместимая версия не установлена. Tест не выполнится.")] 
        public void CreateDocument_KompasIsStarted()
        {
            var kompasConnector = new KompasConnector();

            kompasConnector.Start();

            var document3D = kompasConnector.CreateDocument3D();

            Assert.NotNull(document3D);

            kompasConnector.Close();
        }

        [Test]
        [TestCase(TestName = "Проверка работы свойства IsActive.")]
        //[Ignore("Совместимая версия не установлена. Tест не выполнится.")] 
        public void IsActive()
        {
            var kompasConnector = new KompasConnector();

            // до запуска 
            Assert.IsFalse(kompasConnector.IsActive, "Не правильно работает до запуска.");

            kompasConnector.Start();

            // после запуска 
            Assert.IsTrue(kompasConnector.IsActive, "Не правильно работает после запуска.");

            kompasConnector.Close();

            // после закрытия 
            Assert.IsFalse(kompasConnector.IsActive, "Не правильно работает после закрытия.");
        }

        [Test]
        [TestCase(TestName = "Проверка активации КОМПАС-3D, если установлена совместимая версия.")]
        //[Ignore("Совместимая версия не установлена. Tест не выполнится.")] 
        public void Start_Installed()
        {
            var kompasConnector = new KompasConnector();

            Assert.DoesNotThrow(() => kompasConnector.Start());

            kompasConnector.Close();
        }

        [Test]
        [TestCase(TestName = "Проверка активации КОМПАС-3D, если не установлена совместимая версия.")]
        [NUnit.Framework.Ignore("Совместимая версия установлена. Tест не выполнится.")]
        public void Start_NotInstalled()
        {
            var kompasConnector = new KompasConnector();

            Assert.Throws<NullReferenceException>(() => kompasConnector.Start());
        }
    }
}

