using Datos;
using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
namespace Logica
{
    public class PersonaService
    {
        private readonly PulsacionesContext _context;
        
        public PersonaService(PulsacionesContext context)
        {
            _context=context;
        }
        public GuardarPersonaResponse Guardar(Persona persona)
        {
        
            try
            { 
             var   personaEncontrada = _context.Personas.Find(persona.Identificacion);
            if (personaEncontrada!=null)
            {
                return new GuardarPersonaResponse("Error, persona reguistrada");
            }
                persona.CalcularPulsaciones();
                _context.Personas.Add(persona);
                _context.SaveChanges();
                return new GuardarPersonaResponse(persona);
            }
            catch (Exception e)
            {
                return new GuardarPersonaResponse($"Error de la Aplicacion: {e.Message}");
            }
            
        }
        public List<Persona> ConsultarTodos()
        {
            List<Persona> personas = _context.Personas.ToList();
            return personas;
        }
        public string Eliminar(string identificacion)
        {
            try
            {
                var persona = _context.Personas.Find(identificacion);
                if (persona != null)
                {
                    _context.Personas.Remove(persona);
                _context.SaveChanges();

                    return ($"El registro {persona.Nombre} se ha eliminado satisfactoriamente.");
                }
                else
                {
                    return ($"Lo sentimos, {identificacion} no se encuentra registrada.");
                }
            }
            catch (Exception e)
            {

                return $"Error de la Aplicación: {e.Message}";
            }
            

        }
        public Persona BuscarxIdentificacion(string identificacion)
        {
            
            Persona persona = _context.Personas.Find(identificacion);
        
            return persona;
        }
        public int Totalizar()
        {
            return _context.Personas.Count();
        }
        public int TotalizarMujeres()
        {
        return _context.Personas.Count(p=>p.Sexo=="F");
        }
        public int TotalizarHombres()
        {
            return _context.Personas.Count(p=>p.Sexo=="M");
        }
    }

    public class GuardarPersonaResponse 
    {
        public GuardarPersonaResponse(Persona persona)
        {
            Error = false;
            Persona = persona;
        }
        public GuardarPersonaResponse(string mensaje)
        {
            Error = true;
            Mensaje = mensaje;
        }
        public bool Error { get; set; }
        public string Mensaje { get; set; }
        public Persona Persona { get; set; }
    }
}