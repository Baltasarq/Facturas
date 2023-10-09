// Facturas (c) 2023 Baltasar MIT License <baltasarq@uvigo.gal>  


using Facturas.Core;
using Avalonia.Controls;


namespace Facturas.UI;


/// <summary>Controlador de la vista principal.</summary>
public partial class MainWindow : Window {
    public MainWindow()
    {
        InitializeComponent();
        
        var btInserta = this.FindControl<Button>( "btInserta" );
        var btElimina = this.FindControl<Button>( "btElimina" );
        var btModifica = this.FindControl<Button>( "btModifica" );
        var btSig = this.FindControl<Button>( "btSig" );
        var btAnt = this.FindControl<Button>( "btAnt" );
        var opSig = this.FindControl<MenuItem>( "opSig" );
        var opAnt = this.FindControl<MenuItem>( "opAnt" );
        var opInserta = this.FindControl<MenuItem>( "opInserta" );
        var opModifica = this.FindControl<MenuItem>( "opModifica" );
        var opElimina = this.FindControl<MenuItem>( "opElimina" );
        var opSalir = this.FindControl<MenuItem>( "opSalir" );

        // Configura
        btSig!.Click += (_, _) => this.OnSig();
        opSig!.Click += (_, _) => this.OnSig();
        btAnt!.Click += (_, _) => this.OnAnt();
        opAnt!.Click += (_, _) => this.OnAnt();
        btInserta!.Click += (_, _) => this.OnInserta();
        opInserta!.Click += (_, _) => this.OnInserta();
        btElimina!.Click += (_, _) => this.OnElimina();
        opElimina!.Click += (_, _) => this.OnElimina();
        btModifica!.Click += (_, _) => this.OnModifica();
        opModifica!.Click += (_, _) => this.OnModifica();
        opSalir!.Click += (_, _) => this.Close();
        
        // Prepara
        this.facturas = new RegistroFacturas();
        this.pos = 0;
        this.Actualiza();
    }

    /// <summary>Inserta una nueva factura, al final.</summary>
    async void OnInserta()
    {
        var dlgInserta = new DlgInserta();

        await dlgInserta.ShowDialog( this );
        
        if ( !string.IsNullOrWhiteSpace( dlgInserta.Empresa ) ) {
            this.pos = this.facturas.Count;            
            this.facturas.Add( new Factura {
                                    Empresa = dlgInserta.Empresa,
                                    Bruto = dlgInserta.Bruto });
        }
        
        this.Actualiza();
    }

    /// <summary>Modifica la factura actual, si es posible.</summary>
    void OnModifica()
    {
        if ( pos >= 0
          && pos < this.facturas.Count )
        {
            var edBruto = this.FindControl<TextBox>( "edBruto" );
            double bruto;

            if ( !double.TryParse( edBruto!.Text!.Trim(), out bruto ) ) {
                bruto = 0.0;
            }
            
            this.facturas.Modifica( this.pos, bruto );
        }
        
        this.Actualiza();
    }
    
    /// <summary>Elimina la factura actual, si es posible.</summary>
    void OnElimina()
    {
        if ( this.pos < this.facturas.Count ) {
            this.facturas.Elimina( this.pos );
        }   
        
        this.Actualiza();
    }
    
    /// <summary>Avanza al siguiente, si es posible.</summary>
    void OnSig()
    {
        if ( this.pos < ( this.facturas.Count - 1 ) ) {
            ++this.pos;
        }
        
        this.Actualiza();
    }
    
    /// <summary>Retrocede al anterior, si es posible.</summary>
    void OnAnt()
    {
        if ( this.pos > 0 ) {
            --this.pos;
        }
        
        this.Actualiza();
    }

    /// <summary>Sincroniza la vista con el estado actual.</summary>
    void Actualiza()
    {
        var edEmpresa = this.FindControl<TextBox>( "edEmpresa" );
        var edBruto = this.FindControl<TextBox>( "edBruto" );
        var edTotal = this.FindControl<TextBox>( "edTotal" );
        var lblTotal = this.FindControl<Label>( "lblTotal" );
        int numFacturas = this.facturas.Count;
        
        if ( this.pos < numFacturas ) {
            var factura = this.facturas.Get( this.pos );
            
            edEmpresa!.Text = factura.Empresa;
            edBruto!.Text = factura.Bruto + "";
            edTotal!.Text = factura.Total + "";
        } else {
            edEmpresa!.Text = "N/D";
            edBruto!.Text = "N/D";
            edTotal!.Text = "N/D";
        }

        lblTotal!.Content = $"Factura {this.pos + 1} / {numFacturas} | Total {this.facturas.Total}€";
    }

    RegistroFacturas facturas;
    int pos;
}
