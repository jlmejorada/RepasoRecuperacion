using APIMaui.VM;

namespace APIMaui.Views;

public partial class ListaPersonasViews : ContentPage
{
	private ListadoPersonasVM vm;
	public ListaPersonasViews()
	{
		InitializeComponent();
		vm = new ListadoPersonasVM();
		BindingContext = vm;
	}

	protected override async void OnAppearing()
	{
		base.OnAppearing();
		vm.cargaLista();
    }
}