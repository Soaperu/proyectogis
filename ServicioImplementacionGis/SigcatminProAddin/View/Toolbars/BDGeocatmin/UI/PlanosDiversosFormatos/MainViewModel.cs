using ArcGIS.Desktop.Framework;
using CommonUtilities;
using CommonUtilities.ArcgisProUtils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace SigcatminProAddin.View.Toolbars.BDGeocatmin.UI.PlanosDiversosFormatos
{
    internal class MainViewModel : INotifyPropertyChanged
    {
        private UserControl _currentView;
        public UserControl CurrentView
        {
            get { return _currentView; }
            set { _currentView = value; OnPropertyChanged(); }
        }

        // Datos seleccionados
        // Selección del Paso 1 (Tipo de Plano)
        private string _selectedTipoPlano;
        public string SelectedTipoPlano
        {
            get { return _selectedTipoPlano; }
            set
            {
                _selectedTipoPlano = value;
                OnPropertyChanged();
                // Al cambiar la selección del Paso 1, recargamos la lista para Paso 2
                CargarPaso2Opciones(_selectedTipoPlano);
            }
        }
        // Selección del Paso 2 (Formato / Plano)
        private string _selectedFormato;
        public string SelectedFormato
        {
            get { return _selectedFormato; }
            set
            {
                _selectedFormato = value;
                OnPropertyChanged();
                // Al cambiar la selección del Paso 2, recargamos la lista para Paso 3
                CargarPaso3Opciones(_selectedFormato);
            }
        }
        // Selección del Paso 3 (Escala)
        public string SelectedEscala { get; set; }

        // === Colecciones para la Vista ===
        public ObservableCollection<string> TiposPlano { get; set; }   // Paso 1
        public ObservableCollection<string> OpcionesPaso2 { get; set; } // Paso 2
        public ObservableCollection<string> OpcionesPaso3 { get; set; } // Paso 3

        // ===== DICCIONARIOS =====
        public Dictionary<string, List<string>> Step2Dict { get; set; }
        public Dictionary<string, List<string>> Step3Dict { get; set; }

        // Comandos
        public ICommand NextCommand { get; }
        public ICommand PreviousCommand { get; }
        public ICommand GraficarCommand { get; }

        private int _currentStep;
        public int CurrentStep
        {
            get { return _currentStep; }
            set { _currentStep = value; OnPropertyChanged(); OnPropertyChanged(nameof(ProgressPercentage)); }
        }

        public double ProgressPercentage
        {
            get { return (_currentStep / 3.0) * 100; }
        }

        public MainViewModel()
        {
            // Inicializamos diccionarios
            InicializarDiccionarios();
            // Inicializar colecciones
            TiposPlano = new ObservableCollection<string>
                {
                    "Plano para Atención Público",
                    "Planos Diversos"
                };
            // Colecciones vacías que se llenarán dinámicamente
            OpcionesPaso2 = new ObservableCollection<string>();
            OpcionesPaso3 = new ObservableCollection<string>();

            // Inicializar comandos
            NextCommand = new RelayCommand(Next, CanGoNext);
            PreviousCommand = new RelayCommand(Previous, CanGoPrevious);
            GraficarCommand = new RelayCommand(Graficar, CanGraficar);

            // Establecer la primera vista
            CurrentView = new Views.Step1View
            {
                DataContext = this
            };
            CurrentStep = 1;
        }

        private void Next(object parameter)
        {
            if (CurrentView is Views.Step1View)
            {
                if (string.IsNullOrEmpty(SelectedTipoPlano))
                {
                    ArcGIS.Desktop.Framework.Dialogs.MessageBox.Show("Por favor, seleccione un tipo de plano.");
                    return;
                }
                CurrentView = new Views.Step2View
                {
                    DataContext = this
                };
                CurrentStep = 2;
            }
            else if (CurrentView is Views.Step2View)
            {
                if (string.IsNullOrEmpty(SelectedFormato))
                {
                    ArcGIS.Desktop.Framework.Dialogs.MessageBox.Show("Por favor, seleccione un formato.");
                    return;
                }
                CurrentView = new Views.Step3View
                {
                    DataContext = this
                };
                CurrentStep = 3;
            }
        }

        private bool CanGoNext(object parameter)
        {
            return true;
        }

        private void Previous(object parameter)
        {
            if (CurrentView is Views.Step2View)
            {
                CurrentView = new Views.Step1View
                {
                    DataContext = this
                };
                CurrentStep = 1;
            }
            else if (CurrentView is Views.Step3View)
            {
                CurrentView = new Views.Step2View
                {
                    DataContext = this
                };
                CurrentStep = 2;
            }
        }

        private bool CanGoPrevious(object parameter)
        {
            return true;
        }

        private async void Graficar(object parameter)
        {
            if (string.IsNullOrEmpty(SelectedEscala))
            {
                ArcGIS.Desktop.Framework.Dialogs.MessageBox.Show("Por favor, seleccione una escala.");
                return;
            }

            // Lógica para graficar o procesar la información seleccionada
            ArcGIS.Desktop.Framework.Dialogs.MessageBox.Show($"Procesando:\nTipo de Plano: {SelectedTipoPlano}\nFormato: {SelectedFormato}\nEscala: {SelectedEscala}");
            var layoutConfiguration = new LayoutConfiguration();
            layoutConfiguration.BasePath = GlobalVariables.ContaninerTemplatesReport;
            var layoutUtils = new LayoutUtils(layoutConfiguration);
            var layoutPath = layoutUtils.DeterminarRutaPlantilla(SelectedTipoPlano);
            await LayoutUtils.AddLayoutPath(layoutPath, "Catastro", GlobalVariables.mapNameCatastro, "plantilla_PV_VA0");
        }

        private bool CanGraficar(object parameter)
        {
            return true;
        }

        // Implementación de INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        /// <summary>
        /// Inicializa los diccionarios para Paso1->Paso2 (Step2Dict)
        /// y Paso2->Paso3 (Step3Dict).
        /// </summary>
        private void InicializarDiccionarios()
        {
            // DICCIONARIO: Paso 1 -> Paso 2
            Step2Dict = new Dictionary<string, List<string>>
            {
                {
                    "Plano para Atención Público",
                    new List<string> { "Formato A4", "Formato A3", "Formato A2", "Formato A0" }
                },
                {
                    "Planos Diversos",
                    new List<string>
                    {
                        "Plano A4 Vertical",
                        "Plano A3 Horizontal", "Plano A3 Vertical",
                        "Plano A2 Horizontal", "Plano A2 Vertical",
                        "Plano A1 Horizontal", "Plano A1 Vertical",
                        "Plano A0 Horizontal", "Plano A0 Vertical"
                    }
                }
            };

            // DICCIONARIO: Paso 2 -> Paso 3
            // Combina "Formato..." y "Plano..." para cada uno
            Step3Dict = new Dictionary<string, List<string>>
            {
                // ============================
                // === A4 ===
                // ============================
                { "Formato A4",
                    new List<string>
                    {
                        "2x2 Km - (1/14000)", "3x3 Km - (1/20000)", "4x4 Km - (1/25000)",
                        "6x6 Km - (1/40000)", "8x8 Km - (1/50000)", "10x10 Km - (1/60000)",
                        "12x12 Km - (1/75000)", "16x16 Km - (1/100000)"
                    }
                },
                { "Plano A4 Vertical",
                    new List<string>
                    {
                        "2x2 Km - (1/14000)", "3x3 Km - (1/20000)", "4x4 Km - (1/25000)",
                        "6x6 Km - (1/40000)", "8x8 Km - (1/50000)", "10x10 Km - (1/60000)",
                        "12x12 Km - (1/75000)", "16x16 Km - (1/100000)"
                    }
                },
                
                // ============================
                // === A3 ===
                // ============================
                { "Formato A3",
                    new List<string> { "32x32 Km - (1/100000)", "16x16 Km - (1/50000)" }
                },
                { "Plano A3 Horizontal",
                    new List<string> { "32x32 Km - (1/100000)", "16x16 Km - (1/50000)" }
                },
                { "Plano A3 Vertical",
                    new List<string> { "32x32 Km - (1/100000)", "16x16 Km - (1/50000)" }
                },

                // ============================
                // === A2 ===
                // ============================
                { "Formato A2",
                    new List<string> { "32x32 Km - (1/100000)", "16x16 Km - (1/50000)" }
                },
                { "Plano A2 Horizontal",
                    new List<string> { "32x32 Km - (1/100000)", "16x16 Km - (1/50000)" }
                },
                { "Plano A2 Vertical",
                    new List<string> { "32x32 Km - (1/100000)", "16x16 Km - (1/50000)" }
                },

                // ============================
                // === A1 ===
                // ============================
                { "Plano A1 Horizontal",
                    new List<string> { "32x32 Km - (1/100000)", "16x16 Km - (1/50000)" }
                },
                { "Plano A1 Vertical",
                    new List<string> { "32x32 Km - (1/100000)", "16x16 Km - (1/50000)" }
                },

                // ============================
                // === A0 ===
                // ============================
                { "Formato A0",
                    new List<string>
                    {
                        "2x2 Km - (1/14000)", "3x3 Km - (1/20000)", "4x4 Km - (1/25000)",
                        "6x6 Km - (1/40000)", "8x8 Km - (1/50000)", "10x10 Km - (1/60000)",
                        "12x12 Km - (1/75000)", "16x16 Km - (1/100000)"
                    }
                },
                { "Plano A0 Horizontal",
                    new List<string>
                    {
                        "2x2 Km - (1/14000)", "3x3 Km - (1/20000)", "4x4 Km - (1/25000)",
                        "6x6 Km - (1/40000)", "8x8 Km - (1/50000)", "10x10 Km - (1/60000)",
                        "12x12 Km - (1/75000)", "16x16 Km - (1/100000)"
                    }
                },
                { "Plano A0 Vertical",
                    new List<string>
                    {
                        "2x2 Km - (1/14000)", "3x3 Km - (1/20000)", "4x4 Km - (1/25000)",
                        "6x6 Km - (1/40000)", "8x8 Km - (1/50000)", "10x10 Km - (1/60000)",
                        "12x12 Km - (1/75000)", "16x16 Km - (1/100000)"
                    }
                }
            };
        }

        /// <summary>
        /// Carga las opciones de Paso 2 (OpcionesPaso2) según la selección de Paso 1 (SelectedTipoPlano).
        /// </summary>
        private void CargarPaso2Opciones(string tipoPlano)
        {
            OpcionesPaso2.Clear();

            if (!string.IsNullOrEmpty(tipoPlano) && Step2Dict.ContainsKey(tipoPlano))
            {
                foreach (var item in Step2Dict[tipoPlano])
                {
                    OpcionesPaso2.Add(item);
                }
            }
            // Limpia la selección previa
            SelectedFormato = null;
            OpcionesPaso3.Clear();
            SelectedEscala = null;
        }

        /// <summary>
        /// Carga las opciones de Paso 3 (OpcionesPaso3) según la selección de Paso 2 (SelectedFormato).
        /// </summary>
        private void CargarPaso3Opciones(string formato)
        {
            OpcionesPaso3.Clear();

            if (!string.IsNullOrEmpty(formato) && Step3Dict.ContainsKey(formato))
            {
                foreach (var item in Step3Dict[formato])
                {
                    OpcionesPaso3.Add(item);
                }
            }
            // Limpia la selección previa de la escala
            SelectedEscala = null;
        }

    }
}
