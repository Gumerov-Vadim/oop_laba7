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
            bool set(int x, int y);
            bool add(int x, int y);
            void set_color(Color color);
            void set_size(int size);
            bool select();
            int get_x();
            int get_y();
            int get_width();
            int get_height();
            //System.Windows.Forms.Button inside();
        }

        //классы для группировки
        public abstract class GroupBase : IObject
        {
            public bool in_group;
            public virtual bool obj_in_group(System.Windows.Forms.Button btn) { return false; }
            public virtual int get_count() { return 1; }
            public abstract List<Control> get_controls();
            public virtual void set_count(int c) { }
            public virtual bool set(int x, int y) { return false; }
            public virtual bool add(int x, int y) { return false; }
            public virtual void set_color(Color color) { }
            public virtual void set_size(int size) { }
            public virtual bool select() { return true; }
            public virtual bool select(bool select) { return true; }
            public virtual GroupBase check_obj(object obj) { return null; }
            public virtual System.Windows.Forms.Button inside() { return null; }
            public virtual System.Windows.Forms.Button inside(object obj) { return null; }
            public abstract List<GroupBase> ungroup();
            public GroupBase() { in_group = false; }
            public GroupBase(bool eq) { in_group = eq; }

            public abstract int get_x();
            public abstract int get_y();
            public abstract int get_width();
            public abstract int get_height();
        }
        public class Group : GroupBase
        {
            public GroupBase[] elements;
            private int size;
            private int width, height;
            private int x, y;
            private bool _select;
            //private int count;
            public int get_size() { return size; }
            //public override int get_count() { return count; }
            //public override void set_count(int c) { count = c; }
            //public void add(GroupBase el)
            //{
            //  for (int i = 0; i < size; i++)
            //    {
            //        if (elements[i] == null) { elements[i] = el; elements[i].set_count(elements[i].get_count() + 1); }
            //        this.count++;
            //    }
            //}
            public Group()
            {
                size = 100;
                //count = 0;
                elements = new GroupBase[100];
            }
            public Group(int n)
            {
                size = n;
                //count = 0;
                elements = new GroupBase[n];
            }
            public Group(List<GroupBase> objects) {
                int objects_count = objects.Count();
                elements = new GroupBase[objects_count];
                if(objects_count > 0)
                {
                    //int minx, miny, maxx, maxy;
                    //int width_l, width_r, height_t, height_b;
                    int lb, rb, tb, bb;

                    GroupBase first_obj = objects.First();
                    //minx = maxx = first_obj.get_x();
                    //miny = maxy = first_obj.get_y();
                    //width_l = width_r = first_obj.get_width();
                    //height_t = height_b = first_obj.get_height();

                    lb = first_obj.get_x() - first_obj.get_width()/2;
                    rb = first_obj.get_x() + first_obj.get_width()/2;
                    bb = first_obj.get_y() - first_obj.get_height()/2;
                    tb = first_obj.get_y() + first_obj.get_height()/2;
                    
                    for (int i = 0; i < objects_count; i++)
                    {
                        GroupBase el = elements[i] = objects[i];

                        if(el.get_x() - el.get_width()/2 < lb) { lb = el.get_x() - el.get_width()/2; }
                        if(el.get_x() + el.get_width()/2 > rb) { rb = el.get_x() + el.get_width()/2; }
                        if(el.get_y() - el.get_height()/2 < bb) { bb = el.get_y() - el.get_height()/2; }
                        if(el.get_y() + el.get_height()/2 > tb) { tb = el.get_y() + el.get_height()/2; }

                    }
                    x = (lb + rb )/ 2;
                    y = (tb + bb )/ 2;
                    width = (rb - lb);
                    height = (tb - bb);
                    //for (int i = 0; i < objects_count; i++)
                    //{
                    //    GroupBase el = elements[i] = objects[i];

                    //    if (el.get_x() <= minx) { 
                    //        if (el.get_x() == minx)
                    //        {
                    //            if (el.get_width() > width_l) { width_l = el.get_width(); }
                    //        }
                    //        else
                    //        {
                    //            width_l = el.get_width();
                    //        }
                    //        minx = el.get_x();
                    //    }
                    //    if (el.get_y() <= miny) { miny = el.get_y();
                    //        if (el.get_y() == miny)
                    //        {
                    //            if (el.get_height() > height_b) { height_b = el.get_height(); }
                    //        }
                    //        else
                    //        {
                    //            height_b = el.get_height();
                    //        }
                    //    }
                    //    if (el.get_x() >= maxx) { maxx = el.get_x();
                    //        if (el.get_x() == maxx)
                    //        {
                    //            if (el.get_width() > width_r) { width_r = el.get_width(); }
                    //        }
                    //        else
                    //        {
                    //            width_r = el.get_width(); }
                    //        }

                    //    if (el.get_y() >= maxy) { maxy = el.get_y();
                    //        if (el.get_y() == maxy)
                    //        {
                    //            if (el.get_height() > height_b) { height_b = el.get_height(); }
                    //        }
                    //        else
                    //        {
                    //            height_b = el.get_height();
                    //        }
                    //       }
                    //}
                    //x = (minx + maxx + width_l - width_r) / 2;
                    //y = (miny + maxy + height_t - height_b )/ 2;
                    //width = (width_l/2) + (width_r/2) + maxx - minx;
                    //height = (height_t/2) + (height_b/2) + maxy - miny;
                }
                size = objects_count;
                _select = true;

            }
            override public bool obj_in_group(System.Windows.Forms.Button btn) {
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
            public override int get_count()
            {
                int c = 0;
                foreach(Group el in elements) { c = c + el.get_count(); }
                return c;
            }
            override public bool set(int x, int y) {


                int pw = width/2 ;
                int ph = height/2;
                if (x > pw && x <= 1080 - pw - 22 && y > ph + 22 && y <= 720 - 40 - ph)
                {
                    for(int i = 0; i < size; i++)
                    {
                        elements[i].add(x-this.x, y-this.y);
                    }
                    this.x = x; this.y = y;
                    return true;
                }
                return false;


                //CommandLog history = new CommandLog();
                //SetCommand cmd = new SetCommand(x, y);
                //for(int i = 0; i < size; i++)
                //{
                //    if(elements[i] != null&&!cmd.execute(elements[i]))
                //    {
                //        history.undo_all();
                //        return false;
                //    }
                //    history.add(cmd);
                //}
                //return true;
            }
            override public bool add(int x, int y)
            {
                return this.set(this.x+x,this.y+y);
                

                //CommandLog history = new CommandLog();
                //MoveCommand cmd = new MoveCommand(x,y);
                //for (int i = 0; i < size; i++)
                //{
                //    if (elements[i] != null && !cmd.execute(elements[i]))
                //    {
                //        history.undo_all();
                //        return false;
                //    }
                //    history.add(cmd);
                //}
                //return true;

                //for (int i = 0; i < size; i++)
                //{
                //    if (elements[i] != null && !elements[i].add(x, y))
                //    {
                //        for(int j = 0; j <= i; j++)
                //        {
                //            elements[j].add(-x, -y);
                //        }
                //        return false;
                //    }
                //}
                //return true;
            }
            override public void set_color(Color color)
            {
                for (int i = 0; i < size; i++)
                {
                    if (elements[i] != null)
                    {
                        elements[i].set_color(color);
                    }
                }
            }
            override public void set_size(int size)
            {
                for (int i = 0; i < this.size; i++)
                {
                    if (elements[i] != null)
                    {
                        elements[i].set_size(size);
                    }
                }
            }
            public override bool select(){ return _select;}
            override public bool select(bool select)
            {
                _select = select;
                for (int i = 0; i < size; i++)
                {
                    if (elements[i] != null)
                    {
                        elements[i].select(select);
                    }
                }
                return select;
            }
            public override GroupBase check_obj(object o) {
                GroupBase obj = null;
                for(int i = 0; i < size; i++)
                {
                    obj = elements[i].check_obj(o);
                    if(obj != null) { return this; }
                }
                return null;
            }
            public override List<Control> get_controls() { 
                List<Control> controls = new List<Control>();
                for(int i = 0; i < size; i++)
                {
                    controls = controls.Concat(elements[i].get_controls()).ToList();
                }
                return controls; }
            public override List<GroupBase> ungroup()
            {
                List<GroupBase> elements = new List<GroupBase>();
                for(int i = 0;i < size; i++)
                {
                    elements.Add(this.elements[i]);
                }
                return elements;
            }
            public override System.Windows.Forms.Button inside(object obj)
            {
                for(int i = 0; i < size; i++)
                {
                    if (elements[i] == obj) { return elements[i].inside(); }
                }
                return null;
            }
            public override int get_x() { return x; }
            public override int get_y() { return y; }
            public override int get_width() { return width; }
            public override int get_height() { return height; }
        }

        //классы команд
        public abstract class Command
        {
            public virtual bool execute() { return false; }
            public virtual bool unexecute() { return false; }
            public virtual bool execute(GroupBase o) { return false; }
            public virtual bool unexecute(GroupBase o) { return false; }
            public virtual bool execute(List<GroupBase> o) { return false; }
            public virtual bool unexecute(List<GroupBase> o) { return false; }
            public virtual Command clone() { return null; }
        }
        public class CommandLog
        {
            List<Command> commands;
            public CommandLog() { commands = new List<Command>(); }
            public CommandLog(List<Command> commands) { this.commands = commands; }
            public void add(Command command) { commands.Add(command); }
            public void add(List<Command> command) { this.commands = commands.Concat(command).ToList(); }
            public int count() { return commands.Count; }
            public void undo() { Command unexecuted_command = commands.Last(); unexecuted_command.unexecute(); commands.Remove(unexecuted_command); }
            public void undo(int n) {

                Command unexecuted_command;
                for (int i = 0; i < n; i++)
                {
                    if(commands.Count == 0) { break; }
                    unexecuted_command = commands.Last();
                    unexecuted_command.unexecute();
                    commands.Remove(unexecuted_command);
                }
            }
            public void undo_all() {
                Command unexecuted_command;
                while (commands.Count > 0)
                {
                    unexecuted_command = commands.Last();
                    unexecuted_command.unexecute();
                    commands.Remove(unexecuted_command);
                }
            }
        }
        public class MoveCommand : Command
        {
            private int dx, dy, speed;
            private GroupBase o;
            public override bool execute(GroupBase o) { this.o = o; return o.add(dx * speed, dy * speed); }
            public override bool unexecute(GroupBase o) { this.o = o; return o.add(-dx * speed, -dy * speed);  }
            public override bool execute() { return o.add(dx * speed, dy * speed); }
            public override bool unexecute() { return o.add(-dx * speed, -dy * speed); }
            public MoveCommand(GroupBase o, int dx, int dy) { this.o = o; this.dx = dx; this.dy = dy; this.speed = 1; }
            public MoveCommand(GroupBase o, int dx, int dy, int speed) { this.o = o; this.dx = dx; this.dy = dy; this.speed = speed; }
            public MoveCommand(int dx, int dy) { this.o = null; this.dx = dx; this.dy = dy; this.speed = 1; }
            public MoveCommand(int dx, int dy, int speed) { this.o = null; this.dx = dx; this.dy = dy; this.speed = speed; }
            public override Command clone() { return new MoveCommand(o, dx, dy,speed); }
        }
        public class MoveSelectedCommand : Command
        {
            private int dx, dy, speed;
            List<GroupBase> objects;
            private void set_p(List<GroupBase> objects,int dx,int dy,int speed) { this.objects = objects;this.dx = dx;this.dy = dy;this.speed = speed; }
            private bool exe(List<GroupBase> objects,int dx,int dy,int speed)
            {
                int n = objects.Count;
                int i;
                for (i = 0; i < n; i++)
                {
                    if (!objects[i].add(dx * speed, dy * speed))
                    {
                        for (int j = 0; j < i; j++)
                        {
                            objects[i].add(-dx * speed, -dy * speed);
                        }
                        return false;
                    }
                }
                return true;
            }
            public override bool execute() {
                return exe(objects,dx,dy,speed);
                //int n = objects.Count;
                //int i;
                //for (i = 0; i < n; i++) {
                //    if (!objects[i].add(dx * speed, dy * speed)) {
                //        for(int j=0; j<i; j++)
                //        {
                //            objects[i].add(-dx * speed, -dy * speed);
                //        }
                //        return false; }
                //}
                //return true;

                //bool eq = true;
                //foreach (var o in objects) {
                //    if (!o.add(dx * speed, dy * speed)) { eq = false;}
                //}
                //return eq;
            }
            public override bool unexecute() { 
                return exe(objects, -dx,-dy,speed);
                
                //foreach (var o in objects) { o.add(-dx * speed, -dy * speed); } return true; 
            }
            public override bool execute(List<GroupBase> objects) { 
                this.objects = objects;
                return exe(objects,dx,dy,speed);
                //foreach (var o in objects) { o.add(dx * speed, dy * speed); } return true;
                }
            public override bool unexecute(List<GroupBase> objects) {
                return exe(objects, -dx, -dy, speed);
                //this.objects = objects; foreach (var o in objects) { o.add(-dx * speed, -dy * speed); } return true;
                }
            public MoveSelectedCommand(List<GroupBase> objects, int dx, int dy) { set_p(objects, dx, dy, 1); }
            public MoveSelectedCommand(List<GroupBase> objects, int dx, int dy, int speed) { set_p(objects, dx, dy, speed); }
            public MoveSelectedCommand(int dx, int dy) { set_p(new List<GroupBase>(),dx, dy, 1); }
            public MoveSelectedCommand(int dx, int dy, int speed) { set_p(new List<GroupBase>(), dx, dy, speed); }
            public override Command clone() { return new MoveSelectedCommand(objects,dx, dy,speed); }
        
        }
        public class MoveCommandMap
        {
            private Dictionary<Keys, MoveCommand> move_map;
            private Dictionary<Keys, MoveSelectedCommand> move_selected_map;
            private void fill_the_maps(int speed)
            {
                move_map = new Dictionary<Keys, MoveCommand>();
                move_map.Add(Keys.W, new MoveCommand(0, -1, speed));
                move_map.Add(Keys.A, new MoveCommand(-1, 0, speed));
                move_map.Add(Keys.S, new MoveCommand(0, 1, speed));
                move_map.Add(Keys.D, new MoveCommand(1, 0, speed));

                move_selected_map = new Dictionary<Keys, MoveSelectedCommand>();
                move_selected_map.Add(Keys.W, new MoveSelectedCommand(0, -1, speed));
                move_selected_map.Add(Keys.A, new MoveSelectedCommand(-1, 0, speed));
                move_selected_map.Add(Keys.S, new MoveSelectedCommand(0, 1, speed));
                move_selected_map.Add(Keys.D, new MoveSelectedCommand(1, 0, speed));


            }
            public MoveCommandMap()
            {
                fill_the_maps(1);
            }
            public MoveCommandMap(int speed)
            {
                fill_the_maps(speed);
            }
            public Command move(GroupBase o, KeyEventArgs key) { if (o == null) { return null; } MoveCommand kyda; if (move_map.TryGetValue(key.KeyCode, out kyda)) { kyda.execute(o); return kyda; } return null; }
            public Command move(List<GroupBase> o, KeyEventArgs key) { if (o == null) { return null; } MoveSelectedCommand kyda; if (move_selected_map.TryGetValue(key.KeyCode, out kyda)) { kyda.execute(o); return kyda; } return null; }
            
            //public List<Command> move(Storage storage, KeyEventArgs key) {
            //    List<Command> commands = new List<Command>();
            //    for (int i = 0; i < storage.size(); i++) {

            //        Command command = new MoveCommandMap().move(storage.get(i),key);
            //        commands.Add(command);
            //            }
            //    return commands;
            //}
        }
        //идеи для команд
        public class SelectCommand { }
        public class SetCommand: Command
        {
            private int x1, y1, x2,y2;
            private GroupBase o;
            public override bool execute() { o.set(x2, y2);return true; }
            public override bool unexecute() { o.set(x1, y1); return true; }
            public override bool execute(GroupBase o) { this.o = o;  o.set(x2, y2); return true; }
            public override bool unexecute(GroupBase o) { this.o = o; o.set(x1, y1); return true; }
            public SetCommand(GroupBase o, int x2, int y2) { this.o = o; this.x2 = x2; this.y2 = y2; }
            public SetCommand(int x2, int y2) { this.o = null; this.x2 = x2; this.y2 = y2; }

        }

        //Объекты
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
            public override bool set(int x, int y)
            {
                int p = _size / 2;
                if (x > p && x <= 1080 - p -22 && y > p + 22 && y <= 720 -40 - p)
                {
                    _x = x; _y = y;
                    obj.Location = new System.Drawing.Point(x - p, y - p);
                    return true;
                }
                return false;
            }
            public override bool add(int x, int y){return this.set(_x + x, _y + y);}
            public override void set_color(Color color)
            {
                _color = color;
                obj.BackColor = _color;
            }
            public override void set_size(int size) { _ = size > 0 ? _size = size : size; }
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
            public override GroupBase check_obj(object o)
            {
                if(this.obj == o) { return this; }
                return null;
            }
            public override System.Windows.Forms.Button inside()
            {
                return obj;
            }
            //public override bool obj_in_group(System.Windows.Forms.Button btn)
            //{
            //    return btn.Name == this.obj.Name;
            //}
            public override bool select()
            {
                return _selected;
            }
            public override bool select(bool _select)
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
            public override List<Control> get_controls() {
                List<Control> controls = new List<Control>();
                controls.Add(obj);
                return controls;
            }
            public override List<GroupBase> ungroup()
            {
                List<GroupBase> elem = new List<GroupBase>();
                elem.Add(this);
                return elem;
            }
            public override int get_x() { return _x; }
            public override int get_y() { return _y; }
            public override int get_width() { return _size; }
            public override int get_height() { return _size; }
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
            //public int group_count;
            public List<GroupBase> massive;
            public int size() { return massive.Count; }
            public int count()
            {
                int c = 0;
                foreach(GroupBase obj in massive)
                {
                    c = c + obj.get_count();
                }
                return c;
            }
            public List<GroupBase> get_selected()
            {
                List<GroupBase> selected_objects = new List<GroupBase>();
                for (int i = 0; i < size(); i++)
                {
                    if (massive[i].select()) { selected_objects.Add(massive[i]); }
                }
                return selected_objects;
            }
            public void add(GroupBase obj)
            {
                massive.Add(obj);
                this.select_clear();
                massive.Last().select(true);
            }
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
            public List<Control> del_selected()
            {
                List<Control> deleted = new List<Control>();
                int i = 0;
                while (i < massive.Count)
                {
                    if (massive[i].select())
                    {
                        deleted = deleted.Concat(massive[i].get_controls()).ToList();
                        massive.RemoveAt(i);
                    }
                    else
                    {
                        i++;
                    }
                }
                return deleted;
            }
            public GroupBase get(int i)
            {
                if (i >= 0 && i < size()) { 
                    return massive[i];
                }
                return null;
            }
            //доделать функцию на проверку наличия sender в хранилище
            public GroupBase check_obj(object o) {
                GroupBase obj;
                for(int i = 0; i < size(); i++)
                {
                    obj = massive[i].check_obj(o);
                    if (obj != null) { return obj; }
                }
                return null;
            }

            public GroupBase group()
            {
                List<GroupBase> selected = get_selected();
                foreach (GroupBase obj in selected) {
                obj.in_group = true;
                }

                GroupBase group = new Group(selected);
                del_selected();
                massive.Add(group);
                return group;

                //int selected_count = 0;
                //for (int i = 0; i < size(); i++)
                //{
                //    if (massive[i].select()) { selected_count++; }
                //}
                //GroupBase group = new Group(selected_count);
                //return group;
            }
            public List<GroupBase> ungroup()
            {
                List<GroupBase> selected = get_selected();
                List<GroupBase> ungrouped = new List<GroupBase>();
                while (selected.Count!=0) { 
                    ungrouped = ungrouped.Concat(selected.First().ungroup()).ToList();
                    massive.Remove(selected.First());
                    selected.Remove(selected.First());
                }
                massive = massive.Concat(ungrouped).ToList();

                return ungrouped;
                //foreach (GroupBase obj in selected)
                //{
                //    obj.in_group = true;
                //}

                //GroupBase group = new Group(selected);
                //del_selected();
                //massive.Add(group);
                //return group;
            }
                public Storage()
            {
                massive = new List<GroupBase>();
            }
        }
        Storage storage = new Storage();
        Group grouplist = new Group();
        Settings obj_settings = new Settings();
        MoveCommandMap movemap = new MoveCommandMap(4);
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
            
            obj = storage.check_obj(sender);
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
                List<Control> deleted = storage.del_selected();
                foreach (Control obj in deleted)
                {
                    Controls.Remove(obj);
                }
            }
        }
        public void move_obj(object sender, KeyEventArgs e)
        {
            List<GroupBase> o = storage.get_selected();
            //o = storage.check_obj(sender);
            movemap.move(o, e);

        }
        private void create_group(object sender,KeyEventArgs e)
        {
            //if (storage.group_count!=0)
            //{

            //    int s = 0;
            //    for (int i = 0; i < storage.size(); i++)
            //    {
            //        if (storage.get(i).select()) { s++; }
            //    }
            //    Group g = new Group(s);
            //    for (int i = 0; i < storage.size(); i++)
            //    {
            //        GroupBase obj = storage.get(i);
            //        if (obj.select()) { obj.in_group = true; g.add(obj); }
            //    }
            //    grouplist.add(g);
            //}
            if (e.KeyCode == Keys.M)
            {
                storage.group();
            }
            
        }
        private void paint(object sender, EventArgs e)
        {
            int size = storage.size();
            int k = 0;
            GroupBase circle = null;
            Controls.Clear();
            InitializeComponent();
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

        private void Form1_Load_1(object sender, EventArgs e)
        {

        }

        private void сгруппироватьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            storage.group();
        }

        private void разгруппироватьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            storage.ungroup();
        }
    }
}
