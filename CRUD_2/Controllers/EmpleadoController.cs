using CRUD_2.Data;
using CRUD_2.Models;
using Microsoft.AspNetCore.Mvc;

namespace CRUD_2.Controllers
{
    public class EmpleadoController : Controller
    {
        private readonly EmpleadoDbContext context;

        public EmpleadoController(EmpleadoDbContext Context)
        {
            context = Context;
        }
        [HttpGet]
        public IActionResult Index()
        {
            var listaEmpleados = context.Empleados.ToList();
            List<EmpleadoViewModel> listaEmpViewModel = new List<EmpleadoViewModel>();
            if (listaEmpleados is not null)
            {                
                foreach (var empl in listaEmpleados)
                {
                    EmpleadoViewModel modelo = new EmpleadoViewModel()
                    {
                        Id = empl.Id,
                        Nombre = empl.Nombre,
                        Apellido = empl.Apellido,
                        Email = empl.Email,
                        FechaNacimiento = empl.FechaNacimiento,
                        Salario = empl.Salario,
                    };
                    listaEmpViewModel.Add(modelo);                    
                }
                return View(listaEmpViewModel);
            }
            return View();            
        }
    }
}
