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

        [HttpPost]
        public IActionResult Editar(EmpleadoViewModel modelo)
        {
            try
            {
                var empleadoBuscado = context.Empleados.SingleOrDefault(x=>x.Id == modelo.Id);
                if(empleadoBuscado != null)
                {
                    if (ModelState.IsValid)
                    {
                        //empleadoBuscado.Id = modelo.Id;
                        empleadoBuscado.Nombre = modelo.Nombre;
                        empleadoBuscado.Apellido = modelo.Apellido;
                        empleadoBuscado.FechaNacimiento = modelo.FechaNacimiento;
                        empleadoBuscado.Email = modelo.Email;
                        empleadoBuscado.Salario = modelo.Salario;                       
                        
                        context.Empleados.Update(empleadoBuscado);
                        context.SaveChanges();
                        TempData["successMessage"] = "Empleado fue actualizado con exito!";
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        TempData["errorMessage"] = "Los datos son invalidos!";
                        return View();
                    }
                }
                else
                {
                    TempData["errorMessage"] = $"Detalle no disponible del empleado con id {modelo.Id}";
                    return RedirectToAction("Index");
                }
                
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();
            }
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            try
            {
                var empleado = context.Empleados.SingleOrDefault(x => x.Id == id);
                if (empleado != null)
                {
                    var empleadoViewModel = new EmpleadoViewModel()
                    {
                        Id = empleado.Id,
                        Nombre = empleado.Nombre,
                        Apellido = empleado.Apellido,
                        FechaNacimiento = empleado.FechaNacimiento,
                        Email = empleado.Email,
                        Salario = empleado.Salario
                    };
                    return View(empleadoViewModel);
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

        [HttpPost]
        public IActionResult Delete(EmpleadoViewModel modelo)
        {
            try
            {
                var empleadoDelete = context.Empleados.SingleOrDefault(x => x.Id == modelo.Id);
                if (empleadoDelete is not null)
                {
                    context.Empleados.Remove(empleadoDelete);
                    context.SaveChanges();
                    TempData["successMessage"] = "Empleado borrado exitosamente!";
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["errorMessage"] = $"Empleado no disponible con id {modelo.Id}";
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
