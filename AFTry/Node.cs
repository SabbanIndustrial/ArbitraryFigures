using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace AFTry
{
    /// <summary>
    /// Вершина, наследуется от Button.
    /// </summary>
    public class Node : Button
    {
        /// <summary>
        /// Конструктор вершины, наследуется от конструктора кнопки по умолчанию. 
        /// </summary>
        /// <param name="X">Координата по оси абсцисс.</param>
        /// <param name="Y">Координата по оси ординат.</param>
        /// <param name="c">GControl, на которой создаётся вершина.</param>
        public Node(int X, int Y, GControl c) : base()
        {
            // Задание внешнего вида кнопки
            parent = c;
            Location = new Point(X, Y);
            int radius = 40;
            Size = new Size(radius, radius);
            BackColor = Color.CadetBlue;
            FlatStyle = FlatStyle.Flat;
            FlatAppearance.BorderSize = 0;

            // Делает кнопку круглой
            GraphicsPath buttonPath = new GraphicsPath(); // Создание формы кнопки.
            Rectangle newRectangle = this.ClientRectangle; // Получение прямоугольника из кнопки.
            buttonPath.AddEllipse(newRectangle); // Созаёт на форме кнопки эллипс с границами в прямоугольнике.
            Region = new Region(buttonPath); // Присвоение новой формы кнопке.
        }

        /// <summary>
        /// Определяет, не находится ли Node в процессе перемещения
        /// </summary>
        bool isDown;

        public GControl parent; // всё понятно
        /// <summary>
        /// Объявление события с обобщённым делегатом Action без параметров.
        /// Вызывается при изменении позиции вершины, для последующей корректировки Link.
        /// </summary>
        public event Action LocationChanged; 

        /// <summary>
        /// Свойство Location, скрывает базовое свойство блягодаря new. Делает поправку на то, что координаты отсчитываются не от центра.
        /// </summary>
        public new Point Location
        {
            get
            {
                return new Point(base.Location.X + this.Size.Width / 2, base.Location.Y + this.Size.Height / 2); // Т.к скрыт базовый Location, 
                // получаем позицию вершины от родительского элемента посредством base.
            }
            set
            {
                LocationChanged?.Invoke(); // Вызов события ?. - оператор проверки на наличие обработчиков, Invoke - Вызывает метод в главном потоке обработки WinForms
                base.Location = new Point(value.X - this.Size.Width / 2, value.Y - this.Size.Height / 2);
            }
        }



        /// <summary>
        /// Обработчик события при отпускании кнопки мыши, переопределён с родительского.
        /// </summary>
        /// <param name="mevent"></param>
        protected override void OnMouseDown(MouseEventArgs mevent)
        {
            isDown = true;

            base.OnMouseDown(mevent);
        }
        /// <summary>
        /// Обработчик события при зажатой кнопке мыши, переопределён с родительского.
        /// </summary>
        /// <param name="mevent"></param>
        protected override void OnMouseMove(MouseEventArgs mevent)
        {
            if (isDown && (parent as GControl).SelectedTool == Tool.Move)// Если в зажата кнопка на Node и инструмент == перемещение.
            {
                Point mouse = Parent.PointToClient(MousePosition); 
                this.Location = mouse; // Позиция вершины = позиция курсора.
            }
            base.OnMouseMove(mevent);
        }
        /// <summary>
        /// Обработчик события при нажатии кнопки мыши, переопределён с родительского.
        /// </summary>
        /// <param name="mevent"></param>
        protected override void OnMouseUp(MouseEventArgs mevent)
        {
            isDown = false;
            base.OnMouseUp(mevent);
        }

        /// <summary>
        /// Обработчик клика, используется для создания связей, переопределён с родительского.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnClick(EventArgs e)
        {
            parent.AddLink(this);
            base.OnClick(e);
        }

    }
}
