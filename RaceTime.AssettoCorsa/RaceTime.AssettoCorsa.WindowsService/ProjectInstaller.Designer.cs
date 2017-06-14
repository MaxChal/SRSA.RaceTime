namespace RaceTime.AssettoCorsa.WindowsService
{
    partial class ProjectInstaller
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.AssettoCorsaProcessInstaller = new System.ServiceProcess.ServiceProcessInstaller();
            this.AssettoCorsaServiceInstaller = new System.ServiceProcess.ServiceInstaller();
            // 
            // AssettoCorsaProcessInstaller
            // 
            this.AssettoCorsaProcessInstaller.Account = System.ServiceProcess.ServiceAccount.LocalSystem;
            this.AssettoCorsaProcessInstaller.Password = null;
            this.AssettoCorsaProcessInstaller.Username = null;
            // 
            // AssettoCorsaServiceInstaller
            // 
            this.AssettoCorsaServiceInstaller.Description = "A server that hosts the Assetto Corsa RaceTime Server Plugin";
            this.AssettoCorsaServiceInstaller.DisplayName = "RaceTime AssettoCorsaService";
            this.AssettoCorsaServiceInstaller.ServiceName = "RaceTime.AssettoCorsaService";
            // 
            // ProjectInstaller
            // 
            this.Installers.AddRange(new System.Configuration.Install.Installer[] {
            this.AssettoCorsaProcessInstaller,
            this.AssettoCorsaServiceInstaller});

        }

        #endregion

        private System.ServiceProcess.ServiceProcessInstaller AssettoCorsaProcessInstaller;
        private System.ServiceProcess.ServiceInstaller AssettoCorsaServiceInstaller;
    }
}