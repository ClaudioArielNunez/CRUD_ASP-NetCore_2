using CRUD_2.Data;
using CRUD_2.Models;
using CRUD_2.Models.Entidades;
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

        [HttpGet]
        public IActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Crear(EmpleadoViewModel modelo)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var empleado = new Empleado()
                    {
                        //Id = modelo.Id,
                        Nombre = modelo.Nombre,
                        Apellido = modelo.Apellido,
                        Email = modelo.Email,
                        FechaNacimiento = modelo.FechaNacimiento,
                        Salario = modelo.Salario
                    };
                    context.Empleados.Add(empleado);
                    context.SaveChanges();
                    TempData["successMessage"] = "Empleado creado con exito!";
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["errorMessage"] = "El modelo enviado no es válido";
                    return View();
                }
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();
            }                               
        }

        [HttpGet]
        public IActionResult Editar(int id)
        {
            try
            {
                var empleado = context.Empleados.SingleOrDefault(x => x.Id == id);
                if (empleado != null)
                {
                    var empleadoView = new EmpleadoViewModel()
                    {
                        Id = empleado.Id,
                        Nombre = empleado.Nombre,
                        Apellido = empleado.Apellido,
                        Email = empleado.Email,
                        FechaNacimiento = empleado.FechaNacimiento,
                        Salario = empleado.Salario
                    };
                    return View(empleadoView);
                }
                else
                {
                    TempData["errorMessage"] = $"Detalle no disponible del empleado con id {id}";
                    return RedirectToAction("Index");
                }

            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return RedirectToAction("Index");
            }
        }
       
    }
}
