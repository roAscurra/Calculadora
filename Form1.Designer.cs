namespace Calculadora;

partial class Form1
{
    /// <summary>
    ///  Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    ///  Clean up any resources being used.
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

    #region Windows Form Designer generated code

    /// <summary>
    ///  Required method for Designer support - do not modify
    ///  the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        this.btnCinco = new System.Windows.Forms.Button();
        this.btnSeis = new System.Windows.Forms.Button();
        this.btnSiete = new System.Windows.Forms.Button();
        this.btnOcho = new System.Windows.Forms.Button();
        this.btnNueve = new System.Windows.Forms.Button();
        
        this.SuspendLayout();

        // btnCinco
        // 
        this.btnCinco.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
        this.btnCinco.Location = new System.Drawing.Point(70, 190);
        this.btnCinco.Name = "btnCinco";
        this.btnCinco.Size = new System.Drawing.Size(45, 45);
        this.btnCinco.TabIndex = 5;
        this.btnCinco.Text = "5";
        this.btnCinco.UseVisualStyleBackColor = true;
        //this.btnCinco.Click += new System.EventHandler(this.agregarNumero);
        // 

        // btnSeis
        // 
        this.btnSeis.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
        this.btnSeis.Location = new System.Drawing.Point(127, 190);
        this.btnSeis.Name = "btnSeis";
        this.btnSeis.Size = new System.Drawing.Size(45, 45);
        this.btnSeis.TabIndex = 6;
        this.btnSeis.Text = "6";
        this.btnSeis.UseVisualStyleBackColor = true;
        //this.btnSeis.Click += new System.EventHandler(this.agregarNumero);
        // 

        // btnSiete
        // 
        this.btnSiete.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
        this.btnSiete.Location = new System.Drawing.Point(13, 131);
        this.btnSiete.Name = "btnSiete";
        this.btnSiete.Size = new System.Drawing.Size(45, 45);
        this.btnSiete.TabIndex = 7;
        this.btnSiete.Text = "7";
        this.btnSiete.UseVisualStyleBackColor = true;
        //this.btnSiete.Click += new System.EventHandler(this.agregarNumero);
        // 
        
        // btnOcho
        // 
        this.btnOcho.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
        this.btnOcho.Location = new System.Drawing.Point(70, 131);
        this.btnOcho.Name = "btnOcho";
        this.btnOcho.Size = new System.Drawing.Size(45, 45);
        this.btnOcho.TabIndex = 8;
        this.btnOcho.Text = "8";
        this.btnOcho.UseVisualStyleBackColor = true;
        //this.btnOcho.Click += new System.EventHandler(this.agregarNumero);
    
        // btnNueve
        // 
        this.btnNueve.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
        this.btnNueve.Location = new System.Drawing.Point(127, 131);
        this.btnNueve.Name = "btnNueve";
        this.btnNueve.Size = new System.Drawing.Size(45, 45);
        this.btnNueve.TabIndex = 9;
        this.btnNueve.Text = "9";
        this.btnNueve.UseVisualStyleBackColor = true;
        //this.btnNueve.Click += new System.EventHandler(this.agregarNumero);
        // 
        
        // 
        // Form1
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(331, 365);
        
        this.Controls.Add(this.btnCinco);
        this.Controls.Add(this.btnSeis);
        this.Controls.Add(this.btnSiete);
        this.Controls.Add(this.btnOcho);
        this.Controls.Add(this.btnNueve);
        
        this.MaximizeBox = false;
        this.Name = "Form1";
        this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        this.Text = "Calculadora by GFCSoft";
        this.ResumeLayout(false);
        this.PerformLayout();
    }

    #endregion
    
    private System.Windows.Forms.Button btnCinco;
    private System.Windows.Forms.Button btnSeis;
    private System.Windows.Forms.Button btnSiete;
    private System.Windows.Forms.Button btnOcho;
    private System.Windows.Forms.Button btnNueve;

        
}
