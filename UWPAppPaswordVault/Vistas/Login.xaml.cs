using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// La plantilla de elemento Página en blanco está documentada en http://go.microsoft.com/fwlink/?LinkId=234238

namespace UWPAppPaswordVault.Vistas
{
    /// <summary>
    /// Una página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
    /// </summary>
    public sealed partial class Login : Page
    {
        public Login()
        {
            this.InitializeComponent();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            //Logear al usuario y 
            string resourceName = "UWPAppPasswordVault";
            string usuario = txtNombre.Text;
            string contrasena = txtPassword.Text;

            Windows.Security.Credentials.PasswordCredential credencial = null;

            //guardarlo en el PassportVault si no existe y es el primero
            //--> recuperar del vault la lista
            var vault = new Windows.Security.Credentials.PasswordVault();
            //Controlar exccepcion cuando esta vacia
            //try**
            var credentialList = vault.RetrieveAll();
            
            //var credentialList = vault.FindAllByResource(resourceName);
            //--> mirar si esta vacia
            if (credentialList.Count==0)
            {
                //--> añadirle a la lista
                vault.Add(new Windows.Security.Credentials.PasswordCredential(
                    resourceName, usuario, contrasena));
                Frame.Navigate(typeof(Servicio));
            }
            //Si no es el primero y existe enviarle a Servicio
            //Si no es el primero y no existe mandarle a vienvenida
            if (credentialList.Count > 0)
            {
                //**Si implementamos el try 
                //credencial = credentialList.FirstOrDefault();
                credencial = credentialList[0];

                if (credencial.UserName==usuario && credencial.Resource==resourceName )
                {
                    Frame.Navigate(typeof(Servicio));
                }
                else
                {
                    Frame.Navigate(typeof(BienVenida));
                }
            }
        }

        private void btnBorrar_Click(object sender, RoutedEventArgs e)
        {
            var vault = new Windows.Security.Credentials.PasswordVault();
            var credentialList = vault.RetrieveAll();
            foreach (var item in credentialList)
            {
                vault.Remove(item);
            }
            

        }
    }
}
