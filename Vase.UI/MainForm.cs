using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Vase.UI
{
    public partial class MainForm : Form

    {

        private const string StartKompasAction = "Открытие компас";

        private readonly Dictionary<TextBox, string> _oldValues;

        public MainForm()
        {
            _kompasConnector = new KompasConnector();
            _oldValues = new Dictionary<TextBox, string>();
            InitializeComponent();
        }

        /// <summary>
        /// Название Действия построения
        /// </summary>
        private const string BuildActionName = "Построение вазы";
        /// <summary>
        /// Название действия запуска компаса
        /// </summary>
        private const string StartKompasActionName = "Открытие КОМПАС-3D";
        /// <summary>
        /// Соединитель с компасом
        /// </summary>
        private readonly KompasConnector _kompasConnector;

    

        /// <summary>
        /// Обработчик кнопки запустить
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StartKompasButton_Click(object sender, System.EventArgs e)
        {
            try
            {
                _kompasConnector.Start();
            }
            catch(NullReferenceException ex)
            {
                MessageBox.Show(ex.Message, StartKompasActionName, MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }
        /// <summary>
        /// Обработчик кнопки "Закрыть"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CloseKompasButton_Click(object sender, EventArgs e)
        {
            _kompasConnector.Close();
        }

        private void BuildButton_Click(object sender, EventArgs e)
        {
            var document3D = _kompasConnector.CreateDocument3D()
            var builder = new VaseBuilder(document3D, );
        }
    }

    
}
