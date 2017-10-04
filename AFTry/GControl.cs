using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AFTry
{
    /// <summary>
    /// Поверхность, на которой будут отображаться элементы фигур, наследована от Panel. Можно использовать как обычную панель(но не нужно).
    /// </summary>
    public class GControl : Panel
    {
        /// <summary>
        /// Переменная, типа Tool - enum перечисления инструментов работы с фигурами на панели(добавить, удалить, переместить...).
        /// Её значение - текущий активный инструмент.
        /// </summary>
        public Tool SelectedTool;

        /// <summary>
        /// Конструктор класса, наследуется от конструктора родительского класса(Panel).
        /// </summary>
        public GControl() : base()
        {
            SelectedTool = Tool.AddNode; // Устанавливаем начальный инструмент добавление вершины.
        }
        /// <summary>
        /// Список типа Node - содержит вершины фигур
        /// </summary>
        public List<Node> Nodes = new List<Node>();

        #region Хуита на переделать

        public List<LinkStr> Links = new List<LinkStr>();

        public List<Link> LinkCntr = new List<Link>();
        
        #endregion

        /// <summary>
        /// Позволяет рисовать на любом Control
        /// </summary>
        public Graphics G;


        /// <summary>
        /// Обработчик события клика мыши по панели, используется override - переопределение виртуального метода родительского класса Panel.
        /// В конце данного метода, вызывается переопределённый родительский метод с помощью ключевого слова base.
        /// </summary>
        /// <param name="e">Параметры нажатия(позиция, кнопка...), наследуется от EventArgs.</param>
        protected override void OnMouseClick(MouseEventArgs e)
        {
            G = this.CreateGraphics();
            if (SelectedTool == Tool.AddNode)// Проверка на текущий активный инструмент == добавление вершин.
            {
                Nodes.Add(new Node(0, 0, this)); // Добавляем в список новую вершину.
                Point mouse = PointToClient(MousePosition); // Получение позиции мыши.
                Nodes.Last<Node>().Location = mouse; // Установление для последнего (только что созданного элемента) позиции в позицию курсора мыши.
                Nodes.Last<Node>().Text = $"node {Nodes.Count}"; // Установление для него же текста
                this.Controls.Add(Nodes.Last<Node>()); // Метод начинающий отрисовку Node в пределах данной поверхности(GControl) this необязателен.
            }
            base.OnMouseClick(e); // Вызов родительского метода-обработчика.
        }


        bool acceptLink = false; // Переменная, определяющая очерёдность вершин
        /// <summary>
        /// Метод, позволяющий создавать связи Link между вершинами Node.
        /// </summary>
        /// <param name="a">Поочерёдно две вершины.</param>
        public void AddLink(Node a)
        {
            if (acceptLink == false)// Если Node первый из пары
            {
                Links.Add(new LinkStr(a)); // Добавляем новый LinkStr к списку
                acceptLink = true; // переключаем триггер, определяющий что следующая вершина вторая из пары.
            }
            else
            {
                Links.Last().B = a; // Добавляем к последнему классу списка вторую вершину.
                LinkCntr.Add(new Link(Links.Last().A, Links.Last().B, this)); // Забыл че тут происходит, но это хуйня и я её собираюсь переделывать.
                Controls.Add(LinkCntr.Last());
                acceptLink = false;
                //  DrawLinks();
            }


        }

        /// <summary>
        /// Функция, вызывающаяся при перерисовке поверхности любого Contorl, перопределена с родительского класса.
        /// </summary>
        public void Invalidate()
        {
            base.Invalidate();

        }

        /// <summary>
        /// Пока ничего не делает
        /// </summary>
        public void DrawLinks()
        {
            for (int i = 0; i < Links.Count; i++)
            {
                //Controls.Remove(LinkCntr[i]);
                //  LinkCntr[i].LinkNodes()

            }
        }

    }
    /// <summary>
    /// Перечисление инструментов для работы с фигурами
    /// </summary>
    public enum Tool
    {
        None = 0,
        AddNode,
        Move,
    }

    /// <summary>
    /// Плохое исполнение, но мне пока лень кодить.
    /// Класс, содеражащий 2 экземпляра типа Node. 
    /// Позже воплощу его функционал поизящнее.
    /// </summary>
    public class LinkStr
    {
        public Node A, B;
        public LinkStr(Node a)
        {
            A = a;
            B = a;
        }


    }

}
