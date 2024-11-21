using LibraryProyect.Models;

namespace LibraryProyect.Service
{
    public class UserService
    {

        public User CreateUser(int id, string name, string type)
        {

            string _type = type.ToLower();

            switch (_type)
            {
                case "student": return new Student(id, name);//Crea un usuario estudiante

                case "teacher": return new Teacher(id, name);//Crea un usuario profesor

                default: throw new ArgumentException("Invalid user type");//Esepcion en caso de que el tipo de usuario ingresado no este contemplado

            }


        }
       

    }
}
