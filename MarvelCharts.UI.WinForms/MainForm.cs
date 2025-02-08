namespace MarvelCharts.UI.WinForms
{
    using MaterialSkin;
    using MaterialSkin.Controls;

    public partial class MainForm : MaterialForm
    {
        private MaterialSkinManager _materialSkinManager;
        public MainForm()
        {
            this.InitializeComponent();
            
            this._materialSkinManager = MaterialSkinManager.Instance;
            this._materialSkinManager.AddFormToManage(this);
            this._materialSkinManager.Theme = MaterialSkinManager.Themes.DARK;
            
        }
    }
}
