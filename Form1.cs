using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//using Newtonsoft.Json;
using System.IO;
namespace laba_7
{

    public partial class Form1 : Form
    {
        public class Settings
        {
            private int selected;
            private int size;
            private Color color;
            public System.EventHandler observers;
            public int pick_obj(int i) { _ = (i >= 0) ? selected = i : selected = 0; observers.Invoke(this, null); return selected; }
            public int pick_obj() { return selected; }
            public int resize(int size) { _ = (size >= 10 && size <= 100) ? this.size = size : size = this.size; observers.Invoke(this, null); return size; }
            public int resize() { return size; }
            public void set_color(Color color) { this.color = color; }
            public Color get_color() { return color; }
            public Settings()
            {
                selected = 0;
                size = 30;
                color = Color.Green;
            }
        }
        interface IObject
        {
          //  void set_p(int x, int y, int size, Color color);
            void set(int x, int y);
            void add(int x, int y);
            void set_color(Color color);
            void set_size(int size);
            bool select();
            //System.Windows.Forms.Button inside();
        }
        public abstract class GroupBase : IObject
        {
            public bool in_group;
            public virtual bool obj_in_group(System.Windows.Forms.Button btn) { return false; }
            public virtual int get_count() { return 0; }
            public virtual void set_count(int c) { }
            public virtual void set(int x, int y) { }
            public virtual void add(int x, int y) { }
            public virtual void set_color(Color color) { }
            public virtual void set_size(int size) { }
            public virtual bool select() { return true; }
            public virtual bool select(bool select) { return true; }
            public virtual System.Windows.Forms.Button inside() { return null;}
            public GroupBase() { in_group = false; }
            public GroupBase(bool eq) { in_group = eq; }
        }
        public class Group : GroupBase
        {
            public GroupBase[] elements;
            private int size;
            private bool _select;
            private int count;
            public int get_size() { return size; }
            public int get_count() { return count; }
            public void set_count(int c) { count = c; }
            public void add(GroupBase el)
            {
              for (int i = 0; i < size; i++)
                {
                    if (elements[i] == null) { elements[i] = el; elements[i].set_count(elements[i].get_count() + 1); }
                    this.count++;
                }
            }
            public Group()
            {
                size = 100;
                count = 0;
                elements = new GroupBase[100];
            }
            public Group(int n)
            {
                size = n;
                count = 0;
                elements = new GroupBase[n];
            }
            virtual public bool obj_in_group(System.Windows.Forms.Button btn) {
                int i = 0;bool eq = false;
                while (i < size && !eq) {
                    eq = elements[i].obj_in_group(btn);
                }
                return eq;
            }
            public bool add(Group el)
            {
                for(int i = 0; i < size; i++)
                {
                    if(elements[i] != null) { elements[i] = el;return true; }
                }
                return false;
            }
            virtual public void set(int x, int y) {
                for(int i = 0; i < size; i++)
                {
                    if(elements[i] != null)
                    {
                        elements[i].set(x, y);
                    }
                }
            }
            virtual public void add(int x, int y)
            {
                for (int i = 0; i < size; i++)
                {
                    if (elements[i] != null)
                    {
                        elements[i].add(x, y);
                    }
                }
            }
            virtual public void set_color(Color color)
            {
                for (int i = 0; i < size; i++)
                {
                    if (elements[i] != null)
                    {
                        elements[i].set_color(color);
                    }
                }
            }
            virtual public void set_size(int size)
            {
                for (int i = 0; i < this.size; i++)
                {
                    if (elements[i] != null)
                    {
                        elements[i].set_size(size);
                    }
                }
            }
            public bool select(){ return _select;}
            virtual public bool select(bool select)
            {
                for (int i = 0; i < size; i++)
                {
                    if (elements[i] != null)
                    {
                        elements[i].select(select);
                    }
                }
                return select;
            }
        }
        public abstract class Command
        {
            public abstract void execute();
            public abstract void unexecute();
            public virtual Command clone() { return null; }
        }
        public class MoveCommand
        {
            private int dx, dy;
            public void execute(GroupBase o) { o.add(dx, dy); }
            public void unexecute(GroupBase o) { o.add(-dx, -dy); }
            public MoveCommand(int dx,int dy) { this.dx = dx; this.dy = dy; }
            public MoveCommand clone() { return new MoveCommand(dx,dy); }
        }
        public class AddCommand
        {

        }
        public class Object : GroupBase
        {
            private void set_p(int x, int y, int size, Color color)
            {
                _x = x; _y = y; _size = size;
                _color = color;
                obj.FlatStyle = FlatStyle.Flat;
                obj.FlatAppearance.BorderSize = 0;
                obj.Width = size;
                obj.Height = size;
                obj.Location = new System.Drawing.Point(_x - obj.Width / 2, _y - obj.Height / 2);
                obj.BackColor = color;
            }
            protected int _x, _y, _size;
            protected Color _color;
            public bool _selected;
            public System.Windows.Forms.Button obj = new System.Windows.Forms.Button();
            public void set(int x, int y)
            {
                int p = _size / 2;
                if (x > p && x <= 1080 - p -22 && y > p + 22 && y <= 720 -40 - p)
                {
                    _x = x; _y = y;
                    obj.Location = new System.Drawing.Point(x - p, y - p);
                }
            }
            public void add(int x, int y)
            {
                this.set(_x + x, _y + y);
            }
            public void set_color(Color color)
            {
                _color = color;
                obj.BackColor = _color;
            }
            public void set_size(int size) { _ = size > 0 ? _size = size : size; }
            public Object()
            {
                set_p(10, 10, 60, System.Drawing.Color.Green);
                //circle.Click += new EventHandler(select_circle);

            }
            public Object(int x, int y)
            {
                set_p(x, y, 60, System.Drawing.Color.Green);
            }
            public Object(int x, int y, int size)
            {
                set_p(x, y, size, System.Drawing.Color.Green);
            }
            public Object(int x, int y, int size, Color color)
            {
                set_p(x, y, size, color);
            }
            virtual public System.Windows.Forms.Button inside()
            {
                return obj;
            }
            public override bool obj_in_group(System.Windows.Forms.Button btn)
            {
                return btn.Name == this.obj.Name;
            }
            public bool select()
            {
                return _selected;
            }
            public bool select(bool _select)
            {
                if (_select)
                {
                    this._selected = true;
                    obj.BackColor = System.Drawing.Color.Purple;
                    return true;
                }
                else
                {
                    this._selected = false;
                    obj.BackColor = this._color;
                    return false;
                }
            }
        }
        public class Circle : Object
        {
            private int _radius;
            private void set_p()
            {
                _radius = _size / 2;
                System.Drawing.Drawing2D.GraphicsPath gPath = new System.Drawing.Drawing2D.GraphicsPath();
                gPath.AddEllipse(0, 0, _size, _size);
                Region rg = new Region(gPath);
                obj.Region = rg;
            }
            public Circle() { set_p(); }
            public Circle(int x, int y) : base(x, y) { set_p(); }
            public Circle(int x, int y, int radius) : base(x, y, radius * 2) { set_p(); }
            public Circle(int x, int y, int radius, Color color) : base(x, y, radius * 2, color) { set_p(); }
        }
        public class Square : Object
        {
            public Square() { }
            public Square(int x, int y) : base(x, y) { }
            public Square(int x, int y, int size) : base(x, y, size) { }
            public Square(int x, int y, int size, Color color) : base(x, y, size, color) { }
        }
        public class Triangle : Object
        {
            private void set_p()
            {
                System.Drawing.Drawing2D.GraphicsPath gPath = new System.Drawing.Drawing2D.GraphicsPath();
                gPath.AddPolygon(new[] {
                    new Point(0, obj.Height),
                    new Point(obj.Height, obj.Width),
                    new Point(obj.Width / 2, 0)
                });
                Region rg = new Region(gPath);
                obj.Region = rg;
            }
            public Triangle() { set_p(); }
            public Triangle(int x, int y) : base(x, y) { set_p(); }
            public Triangle(int x, int y, int size) : base(x, y, size) { set_p(); }
            public Triangle(int x, int y, int size, Color color) : base(x, y, size, color) { set_p(); }
        }
        public class Storage
        {
//            int count;
            public int group_count;
            public List<GroupBase> massive;
            public int size() { return massive.Count; }
            public void add(GroupBase obj)
            {
                massive.Add(obj);
            }
            //public void add(Group obj)
            //{
            //    int i = 0;
            //    while (i < _size && massive[i] != null)
            //    {
            //        i++;
            //    }
            //    if (i != _size)
            //    {
            //        massive[i] = obj;
            //    }
            //}
            public void select_clear()
            {
                foreach (GroupBase obj in massive)
                {
                    obj.select(false);
                }    
            }
            public void recolor_selected(Color color)
            {
                foreach (GroupBase obj in massive)
                {
                    if (obj.select()) { obj.set_color(color); }
                }
            }
            public void move_selected(int x, int y)
            {
                foreach (GroupBase obj in massive)
                {
                    if (obj.select()) { obj.add(x, y); }
                }
            }
            public int del_selected()
            {
                int i = 0;
                int k = 0;
                while (i < massive.Count)
                {
                    if (massive[i].select())
                    {
                        massive.RemoveAt(i);
                    }
                    else
                    {
                        i++;
                    }
                }
                return k;
            }
            public GroupBase get(int i)
            {
                if (i >= 0 && i < size()) { 
                    return massive[i];
                }
                return null;
            }
            //доделать функцию на проверку наличия sender в хранилище
            public Object check_obj(Object obj) {
                return null;
            }
            public Storage()
            {
                massive = new List<Object> ();
                group_count = 0;
            }
            //дописать метод для группировки
            public GroupBase group()
            {
                int selected_count = 0;
                for (int i = 0; i < size(); i++)
                {
                    if (massive[i].select()) { selected_count++; }
                }
                GroupBase group = new Group(selected_count);
                return group;
            }
        }
        Storage storage = new Storage();
        Group grouplist = new Group();
        Settings obj_settings = new Settings();
        public Form1()
        {
            InitializeComponent();
            obj_settings.observers += new EventHandler(this.updatefromsettings);
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            int size = storage.size();
            int k = 0;
            while (k < size)
            {
                if (storage.get(k) != null)
                {
                    System.Windows.Forms.Button circle = storage.get(k).inside();
                    circle.MouseClick += select_obj;
                    circle.KeyDown += del_selected_obj;
                }
                k++;
            }
        }
        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
             int p = obj_settings.resize();
            if (e.X > p && e.X <= 1080 - p - 22 && e.Y > p + 22 && e.Y <= 720 - p - 40)
            {
                Object obj = null;
                switch (obj_settings.pick_obj())
                {
                    case 0:
                        obj = new Circle(e.X, e.Y, obj_settings.resize() / 2, obj_settings.get_color());
                        break;
                    case 1:
                        obj = new Triangle(e.X, e.Y, obj_settings.resize(), obj_settings.get_color());
                        break;
                    case 2:
                        obj = new Square(e.X, e.Y, obj_settings.resize(), obj_settings.get_color());
                        break;
                    default:
                        obj = null;
                        //impossible
                        break;
                }
                storage.add(obj);

                if (obj != null)
                {
                    obj.inside().MouseClick += select_obj;
                    obj.inside().KeyDown += del_selected_obj;
                    obj.inside().KeyDown += move_obj;
                    obj.inside().KeyDown += create_group;
                    this.Controls.Add(obj.inside());
                    //                label1.Text = i.ToString();
                    storage.select_clear();
                    obj.select(true);
                }
            }
        }
        public void select_obj(object sender, MouseEventArgs e)
        {
            GroupBase obj = null;
            int k = 0;
            int size = storage.size();
            while (k < size)
            {
                if (storage.get(k) != null && sender == storage.get(k).inside())
                {
                    obj = storage.get(k);
                }
                k++;
            }
            if (Control.ModifierKeys == Keys.Control)
            {
                obj.select(!obj.select());
            }
            else
            {
                storage.select_clear();
                obj.select(true);
            }
        }
        public void del_selected_obj(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                {
                    Object circle = null;
                    int k = 0;
                    int size = storage.size();
                    while (k < size)
                    {
                        circle = storage.get(k);
                        if (circle != null && circle.select())
                        {
                            Controls.Remove(circle.inside());
                        }
                        k++;
                    }
                    storage.del_selected();
                    //                i = i - storage.del_selected();
                    //                label1.Text = i.ToString();
                }
            }
        }
        public void move_obj(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.W)
            {
                storage.move_selected(0, -1);
            }
            if (e.KeyCode == Keys.A)
            {
                storage.move_selected(-1, 0);
            }
            if (e.KeyCode == Keys.S)
            {
                storage.move_selected(0, 1);
            }
            if (e.KeyCode == Keys.D)
            {
                storage.move_selected(1, 0);
            }
        }
        private void create_group(object sender,KeyEventArgs e)
        {
            if (storage.group_count!=0)
            {
                
                int s = 0;
                for (int i = 0; i < storage.size(); i++)
                {
                    if (storage.get(i).select()) { s++; }
                }
                Group g = new Group(s);
                for (int i = 0; i < storage.size(); i++)
                {
                    Object obj = storage.get(i);
                    if (obj.select()) { obj.in_group = true; g.add(obj); }
                }
                grouplist.add(g);
            }
        }
        private void paint(object sender, EventArgs e)
        {
            int size = storage.size();
            int k = 0;
            Object circle = null;
            Controls.Clear();
            while (k < size)
            {
                circle = storage.get(k);
                if (circle != null)
                {
                    Controls.Add(circle.inside());
                }
                k++;
            }
        }
        private void updatefromsettings(object sender, EventArgs e)
        {
            switch (obj_settings.pick_obj())
            {
                case 0:
                    object_picker.Text = "Круг";
                    break;
                case 1:
                    object_picker.Text = "Треугольник";
                    break;
                case 2:
                    object_picker.Text = "Квадрат";
                    break;
            }
            object_picker.Size = new System.Drawing.Size(100, 20);
        }
        private void кругToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            obj_settings.pick_obj(0);
        }
        private void треугольникToolStripMenuItem_Click(object sender, EventArgs e)
        {
            obj_settings.pick_obj(1);
        }
        private void квадратToolStripMenuItem_Click(object sender, EventArgs e)
        {
            obj_settings.pick_obj(2);
        }
        private void size_changer_ValueChanged(object sender, EventArgs e)
        {
            obj_settings.resize((int)size_changer.Value);
        }
        private void color_picker_Click(object sender, EventArgs e)
        {
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                obj_settings.set_color(colorDialog.Color);
                storage.recolor_selected(colorDialog.Color);
            }
        }
        private void add_tree_view(object sender, EventArgs e)
        {
        }
    }
}
