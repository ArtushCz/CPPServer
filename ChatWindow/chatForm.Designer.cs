
using System;

namespace ChatWindow
{
    partial class chatForm
    {
        /// <summary>
        /// Vyžaduje se proměnná návrháře.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Uvolněte všechny používané prostředky.
        /// </summary>
        /// <param name="disposing">hodnota true, když by se měl spravovaný prostředek odstranit; jinak false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Kód generovaný Návrhářem Windows Form

        /// <summary>
        /// Metoda vyžadovaná pro podporu Návrháře - neupravovat
        /// obsah této metody v editoru kódu.
        /// </summary>
        private void InitializeComponent()
        {
            this.inputBox = new System.Windows.Forms.TextBox();
            this.submit = new System.Windows.Forms.Button();
            this.chatWindow = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // inputBox
            // 
            this.inputBox.Location = new System.Drawing.Point(12, 435);
            this.inputBox.Name = "inputBox";
            this.inputBox.Size = new System.Drawing.Size(808, 20);
            this.inputBox.TabIndex = 0;
            this.inputBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.onEnter_submit);
            // 
            // submit
            // 
            this.submit.Location = new System.Drawing.Point(827, 435);
            this.submit.Name = "submit";
            this.submit.Size = new System.Drawing.Size(75, 20);
            this.submit.TabIndex = 1;
            this.submit.Text = "Odeslat";
            this.submit.UseVisualStyleBackColor = true;
            this.submit.Click += new System.EventHandler(this.submit_Click);
            // 
            // chatWindow
            // 
            this.chatWindow.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.chatWindow.Cursor = System.Windows.Forms.Cursors.Default;
            this.chatWindow.ReadOnly = true;
            this.chatWindow.Location = new System.Drawing.Point(12, 12);
            this.chatWindow.Name = "chatWindow";
            this.chatWindow.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.chatWindow.Size = new System.Drawing.Size(890, 414);
            this.chatWindow.TabIndex = 2;
            this.chatWindow.Text = "Chat...\r\n";
            // 
            // chatForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(914, 464);
            this.Controls.Add(this.chatWindow);
            this.Controls.Add(this.submit);
            this.Controls.Add(this.inputBox);
            this.Name = "chatForm";
            this.Text = "Chat";
            this.ResumeLayout(false);
            this.PerformLayout();

        }


        #endregion

        private System.Windows.Forms.TextBox inputBox;
        private System.Windows.Forms.Button submit;
        private System.Windows.Forms.RichTextBox chatWindow;

        
       
    }
}

