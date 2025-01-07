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

            public Modulo(string nombre, Type pagina)
            {
                Nombre = nombre;
                Pagina = pagina;
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
                new Modulo("Áreas Restringidas", typeof(AreasRestringidas)),
                new Modulo("Código Carta Nacional", typeof(CodigoCartaNacional)),
                new Modulo("Código de DM", typeof(CodigoDM)),
                new Modulo("Consulta Límites de Áreas de Interés", typeof(LimiteAreaInteres)),
                new Modulo("Coordenadas del Punto Central", typeof(Coordenadas_Punto_Central)),
                new Modulo("DM por Carta IGN", typeof(DMCartaIGN)),
                new Modulo("DM por Demarcación Política", typeof(DMxDemarcacionPolitica)),
                new Modulo("Varias Cartas Nacionales", typeof(VariasCartasNacional)),
                

                
                

                new Modulo("Límite Departamental", typeof(LimiteDepartamental)),
                new Modulo("Límite Provincial", typeof(LimiteProvincial)),
                new Modulo("Límite Distrital", typeof(LimiteDistrital)),
                //new Modulo("Consulta por Carta", typeof(ConsultaCarta)),
                //new Modulo("Consulta por Demarcación", typeof(ConsultaDemarcacion))
            }),
            new CategoriaModulo("Planos", new List<Modulo>
            {
                //new Modulo("Plano Demarcación", typeof(PlanoDemarcacion)),
                //new Modulo("Plano área Restringida", typeof(PlanoAreaRestringida)),
                //new Modulo("Plano Evaluación", typeof(PlanoEvaluacion))
            }),
            new CategoriaModulo("Evaluación", new List<Modulo>
            {
                new Modulo("Evaluación DM", typeof(EvaluacionDM)),
            })
        };
      }
}

