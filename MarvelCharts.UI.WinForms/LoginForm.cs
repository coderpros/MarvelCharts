namespace MarvelCharts.UI.WinForms
{
    using MaterialSkin;
    using MaterialSkin.Controls;

    public partial class LoginForm : MaterialForm
    {
        private MaterialSkinManager _materialSkinManager;

        public LoginForm()
        {
            this.InitializeComponent();

            this._materialSkinManager = MaterialSkinManager.Instance;
            this._materialSkinManager.AddFormToManage(this);
            this._materialSkinManager.Theme = MaterialSkinManager.Themes.DARK;
        }
    }
}
