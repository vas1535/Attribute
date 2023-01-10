using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;


namespace Attribute1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Human hum = new Human();
            Console.WriteLine(hum.ToString());
        }

    }
    [AttributeUsage(AttributeTargets.All)]
    public class PropertyAttribute : Attribute
    {
        public string _value_name { get; set; }
        public string _value_lastname { get; set; }
        public string _value_dofb { get; set; }
        public PropertyAttribute() { }
        public PropertyAttribute(string pathtoname, string pathtolastname, string pathtodofb)
        {
            try
            {
                StreamReader readername = new StreamReader(pathtoname);
                StreamReader readerlastname = new StreamReader(pathtolastname);
                StreamReader readerdofb = new StreamReader(pathtodofb);
                _value_name = readername.ReadToEnd();
                _value_lastname = readerlastname.ReadToEnd();
                _value_dofb = readerdofb.ReadToEnd();
            }
            catch (NullReferenceException ex)
            {
                throw new ArgumentException(ex.Message, nameof(pathtoname));
            }
        }
    }
    [Property("name.ini", "lastname.ini", "dateofbirth.ini")]
    class Human
    {
        PropertyAttribute MyAttribute;
        [Property("name.ini", "lastname.ini", "dateofbirth.ini")]
        public string Name { get; set; }
        [Property("name.ini", "lastname.ini", "dateofbirth.ini")]
        public string LastName { get; set; }
        [Property("name.ini", "lastname.ini", "dateofbirth.ini")]
        public string Dayofbirth { get; set; }

        public void GetAttribute(Type t)
        {
            PropertyAttribute att;

            // Get the class-level attributes.
            // Put the instance of the attribute on the class level in the att object.
            att = (PropertyAttribute)Attribute.GetCustomAttribute(t, typeof(PropertyAttribute));
            MemberInfo[] MyMemberInfo = t.GetProperties();
            for (int i = 0; i < MyMemberInfo.Length; i++)
            {
                att = (PropertyAttribute)Attribute.GetCustomAttribute(MyMemberInfo[i], typeof(PropertyAttribute));
                if (att == null)
                {
                    Console.WriteLine("No attribute in member function {0}.\n", MyMemberInfo[i].ToString());
                }
                else
                {
                    Console.WriteLine("The Name Attribute for the {0} member is: {1}.",
                        MyMemberInfo[i].ToString(), att._value_name);
                    Console.WriteLine("The Level Attribute for the {0} member is: {1}.",
                        MyMemberInfo[i].ToString(), att._value_lastname);
                    Console.WriteLine("The Reviewed Attribute for the {0} member is: {1}.\n",
                        MyMemberInfo[i].ToString(), att._value_dofb);
                }
            }
        }
        public Human() { }
        public Human(Type t)
        {
            try
            {
                MyAttribute = (PropertyAttribute)Attribute.GetCustomAttribute(t, typeof(PropertyAttribute));
                if (MyAttribute != null)
                {
                    Name = MyAttribute._value_name;
                    LastName = MyAttribute._value_lastname;
                    Dayofbirth = MyAttribute._value_dofb;
                }
            }
            catch (NullReferenceException ex)
            {
                throw new ArgumentException(ex.Message, nameof(MyAttribute));
            }
        }

        public override string ToString()
        {
            return "Значение взято из INI файлов " + Name + " " + LastName + " " + Dayofbirth + " ";
        }

    }
}
