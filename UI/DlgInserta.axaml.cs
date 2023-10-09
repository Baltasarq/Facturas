// Facturas (c) 2023 Baltasar MIT License <baltasarq@uvigo.gal>  


using Facturas.Core;
using Avalonia.Controls;


namespace Facturas.UI;


/// <summary>Controlador de la vista principal.</summary>
public partial class DlgInserta : Window {
    public DlgInserta()
    {
        InitializeComponent();
        
        var btGuarda = this.FindControl<Button>( "btGuarda" );
        var btCancela = this.FindControl<Button>( "btCancela" );
        var edBruto = this.FindControl<TextBox>( "edBruto" );

        // Configura
        btGuarda!.Click += (_, _) => this.OnGuarda();
        btCancela!.Click += (_, _) => this.OnCancela();
        edBruto!.TextChanged += (_, _) => this.OnBrutoModificado();
        
        // Prepara
        this.Empresa = string.Empty;
        this.Bruto = -1;
        this.f = new Factura{ Empresa = "empresa", Bruto = 0 };
    }

    /// <summary>Se va a insertar, preparar las propiedades.</summary>
    void OnGuarda()
    {
        var edEmpresa = this.FindControl<TextBox>( "edEmpresa" );
        var edBruto = this.FindControl<TextBox>( "edBruto" );
        var empresa = ( edEmpresa!.Text ?? string.Empty ).Trim();
        double bruto;
        
        if ( !double.TryParse( edBruto!.Text, out bruto ) ) {
            bruto = 0.0;
        }

        this.Empresa = empresa;
        this.Bruto = bruto;
        this.Close();
    }

    /// <summary>Se cancela, cierra la ventana.</summary>
    void OnCancela()
    {
        this.Close();
    }

    /// <summary>Actualización del total.</summary>
    void OnBrutoModificado()
    {
        var edBruto = this.FindControl<TextBox>( "edBruto" );
        var edTotal = this.FindControl<TextBox>( "edTotal" );
        double bruto;
        
        if ( !double.TryParse( edBruto!.Text, out bruto ) ) {
            bruto = 0.0;
        }

        f.Bruto = bruto;
        edTotal!.Text = $"{f.Total:000.00}";
    }
    
    public string Empresa { get; private set; }
    public double Bruto { get; private set; }
    Factura f;
}
