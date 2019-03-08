using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Vase
{
    public partial class MainForm : Form

    {
        private const string BuildActionName = "Построение вазы";

        private const string StartKompasAction = "Открытие компас";

        /// <summary>
        ///     Название Действия построения
        /// </summary>
        private const string BuildActionName = "Построение вазы";

        /// <summary>
        ///     Название действия запуска компаса
        /// </summary>
        private const string StartKompasActionName = "Открытие КОМПАС-3D";

        /// <summary>
        ///     Соединитель с компасом
        /// </summary>
        private readonly KompasConnector _kompasConnector;

        private readonly Dictionary<TextBox, string> _oldValues;

        public MainForm(KompasConnector kompasConnector)
        {
            _kompasConnector = kompasConnector;
            _oldValues = new Dictionary<TextBox, string>();
            InitializeComponent();
        }


        /// <summary>
        ///     Обработчик кнопки запустить
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StartKompasButton_Click(object sender, EventArgs e)
        {
            try
            {
                _kompasConnector.Start();
            }
            catch (NullReferenceException ex)
            {
                MessageBox.Show(ex.Message, StartKompasActionName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        ///     Обработчик кнопки "Закрыть"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CloseKompasButton_Click(object sender, EventArgs e)
        {
            _kompasConnector.Close();
        }

        private void BuildButton_Click(object sender, EventArgs e)
        {
            var kompas = Kompas
            var builder = new VaseBuilder();
        }
    }
}