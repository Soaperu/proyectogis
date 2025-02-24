using SigcatminProAddin.View.Toolbars.BDGeocatmin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SigcatminProAddin.View.Modulos
{
    internal static class ModuleConfiguration
    {
        internal class Modulo
        {
            public string Nombre { get; set; }
            public Type Pagina { get; set; } // Tipo de página asociado
            public string TituloPagina { get; set; }
            public Modulo(string nombre, Type pagina, string tituloPagina=null)
            {
                Nombre = nombre;
                Pagina = pagina;
                TituloPagina =tituloPagina;
            }
        }

        internal class CategoriaModulo
        {
            public string Categoria { get; set; }
            public List<Modulo> Modulos { get; set; }

            public CategoriaModulo(string categoria, List<Modulo> modulos)
            {
                Categoria = categoria;
                Modulos = modulos;
            }
        }

        public static List<CategoriaModulo> CategoriasModulos = new List<CategoriaModulo>
        {
            new CategoriaModulo("Consultas", new List<Modulo>
            {
                new Modulo("Actualización Cuadriculas Regionales", typeof(ActualizacionCuadriculasRegionales)),
                new Modulo("Acumulación", typeof(Acumulacion)),
                new Modulo("Áreas Restringidas", typeof(AreasRestringidas), "Áreas Restringidas"),
                new Modulo("Código Carta Nacional", typeof(CodigoCartaNacional)),
                new Modulo("Código de DM", typeof(CodigoDM)),
                new Modulo("Coordenadas del Punto Central", typeof(Coordenadas_Punto_Central)),
                new Modulo("División", typeof(Division)),
                new Modulo("DM por Carta IGN", typeof(DMCartaIGN)),
                new Modulo("DM por Demarcación Política", typeof(DMxDemarcacionPolitica)),
                new Modulo("DM por PMA", typeof(DMxPMA)),
                new Modulo("Estadística de Áreas Restringidas", typeof(EstadisticasAreasRestringidas)),
                new Modulo("Límites de Áreas de Interés", typeof(LimiteAreaInteres)),
                new Modulo("Simulación de Petitorios", typeof(SimulaPetitorios)),
                new Modulo("Varias Cartas Nacionales", typeof(VariasCartasNacional)),
                new Modulo("Generación Histórica DM", typeof(GeneracionHistoricaDM)),
                new Modulo("Renuncia DM", typeof(Renuncia)),
                new Modulo("Manto de Areas Restringidas", typeof(MantoAreaRestringida)),


                new Modulo("Límite Departamental", typeof(LimiteDepartamental)),
                new Modulo("Límite Provincial", typeof(LimiteProvincial)),
                new Modulo("Límite Distrital", typeof(LimiteDistrital)),
                //new Modulo("Consulta por Demarcación", typeof(ConsultaDemarcacion))
                new Modulo("Simulación de Petitorios", typeof(SimulaPetitorios)),
                new Modulo("Superpuesto Por día", typeof(SuperpuestoPorDia)),
                new Modulo("Varias Cartas Nacionales", typeof(VariasCartasNacional)),
            }),
            new CategoriaModulo("Planos", new List<Modulo>
            {
                new Modulo("Plano Áreas Restringidas", typeof(AreasRestringidas), "Áreas Restringidas"),
                //new Modulo("Plano área Restringida", typeof(PlanoAreaRestringida)),
                //new Modulo("Plano Evaluación", typeof(PlanoEvaluacion))
            }),
            new CategoriaModulo("Evaluación", new List<Modulo>
            {
                new Modulo("Evaluación DM", typeof(EvaluacionDM)),
                new Modulo("Libre Denunciabilidad", typeof(LibreDenunciabilidad)),
                new Modulo("Simultaneidad de Petitorios", typeof(SimultaneidadPetitorios)),
            })
        };
      }
}

