using GitVisualizer.UI;
using SkiaSharp;

namespace GitVisualizer
{
    /// <summary>
    /// A form to display an error dialog.
    /// </summary>
    partial class ErrorForm
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
            components = new System.ComponentModel.Container();
            notifyIcon1 = new NotifyIcon(components);
            tableLayoutPanel1 = new TableLayoutPanel();
            copyMessageButton = new Button();
            repoPageButton = new Button();
            messageInsert = new Label();
            commandInsert = new Label();
            command = new Label();
            message = new Label();
            copyCommandButton = new Button();
            tableLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // notifyIcon1
            // 
            notifyIcon1.Text = "This will send you to an external site, proceed?";
            notifyIcon1.Visible = true;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 1;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Controls.Add(copyMessageButton, 0, 6);
            tableLayoutPanel1.Controls.Add(repoPageButton, 0, 4);
            tableLayoutPanel1.Controls.Add(messageInsert, 0, 3);
            tableLayoutPanel1.Controls.Add(commandInsert, 0, 1);
            tableLayoutPanel1.Controls.Add(command, 0, 0);
            tableLayoutPanel1.Controls.Add(message, 0, 2);
            tableLayoutPanel1.Controls.Add(copyCommandButton, 0, 5);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 7;
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.Size = new Size(315, 292);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // copyMessageButton
            // 
            copyMessageButton.Dock = DockStyle.Fill;
            copyMessageButton.FlatStyle = FlatStyle.Flat;
            copyMessageButton.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            copyMessageButton.Location = new Point(3, 254);
            copyMessageButton.Name = "copyMessageButton";
            copyMessageButton.Size = new Size(309, 35);
            copyMessageButton.TabIndex = 29;
            copyMessageButton.Text = "Copy Message to Clipboard";
            copyMessageButton.UseVisualStyleBackColor = true;
            copyMessageButton.Click += CopyMessageButton_Click;
            // 
            // repoPageButton
            // 
            repoPageButton.Dock = DockStyle.Fill;
            repoPageButton.FlatStyle = FlatStyle.Flat;
            repoPageButton.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            repoPageButton.Location = new Point(3, 177);
            repoPageButton.Name = "repoPageButton";
            repoPageButton.Size = new Size(309, 33);
            repoPageButton.TabIndex = 25;
            repoPageButton.Text = "GitHub Page";
            repoPageButton.UseVisualStyleBackColor = true;
            repoPageButton.Click += RepoPageButton_Click;
            // 
            // messageInsert
            // 
            messageInsert.AutoSize = true;
            messageInsert.Dock = DockStyle.Fill;
            messageInsert.Location = new Point(3, 108);
            messageInsert.Name = "messageInsert";
            messageInsert.Size = new Size(309, 66);
            messageInsert.TabIndex = 1;
            messageInsert.Text = "label2";
            // 
            // commandInsert
            // 
            commandInsert.AutoSize = true;
            commandInsert.Dock = DockStyle.Fill;
            commandInsert.Location = new Point(3, 21);
            commandInsert.Name = "commandInsert";
            commandInsert.Size = new Size(309, 66);
            commandInsert.TabIndex = 0;
            commandInsert.Text = "label1";
            // 
            // command
            // 
            command.AutoSize = true;
            command.Dock = DockStyle.Fill;
            command.Font = new Font("Segoe UI", 12F, FontStyle.Bold | FontStyle.Underline, GraphicsUnit.Point);
            command.Location = new Point(3, 0);
            command.Name = "command";
            command.Size = new Size(309, 21);
            command.TabIndex = 26;
            command.Text = "Command:";
            // 
            // message
            // 
            message.AutoSize = true;
            message.Dock = DockStyle.Fill;
            message.Font = new Font("Segoe UI", 12F, FontStyle.Bold | FontStyle.Underline, GraphicsUnit.Point);
            message.Location = new Point(3, 87);
            message.Name = "message";
            message.Size = new Size(309, 21);
            message.TabIndex = 27;
            message.Text = "Error/Message:";
            // 
            // copyCommandButton
            // 
            copyCommandButton.Dock = DockStyle.Fill;
            copyCommandButton.FlatStyle = FlatStyle.Flat;
            copyCommandButton.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            copyCommandButton.Location = new Point(3, 216);
            copyCommandButton.Name = "copyCommandButton";
            copyCommandButton.Size = new Size(309, 32);
            copyCommandButton.TabIndex = 28;
            copyCommandButton.Text = "Copy Command to Clipboard";
            copyCommandButton.UseVisualStyleBackColor = true;
            copyCommandButton.Click += CopyCommandButton_Click;
            // 
            // ErrorForm
            // 
            AutoScaleMode = AutoScaleMode.Inherit;
            ClientSize = new Size(315, 292);
            Controls.Add(tableLayoutPanel1);
            Name = "ErrorForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "GitHelper Login";
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion
        #pragma warning disable Commentor // Missing comments.
        public void ApplyColorTheme(UITheme.AppTheme theme)
        #pragma warning restore Commentor // Missing comments.
        {
            BackColor = theme.AppBackground;
            ForeColor = theme.TextSoft;

            command.ForeColor = theme.TextHeader;
            message.ForeColor = theme.TextHeader;
            commandInsert.ForeColor = theme.TextNormal;
            messageInsert.ForeColor = theme.TextNormal;

            /// Apply themes to all buttons
            IEnumerable<Control> buttons = this.Controls.OfType<Control>().Where(x => x is Button);
            foreach (Button button in buttons)
            {
                button.BackColor = theme.ElementBackground;
                button.ForeColor = theme.TextSelectable;
            }
            /// Apply themes to all radio buttons
            IEnumerable<Control> radios = this.Controls.OfType<RadioButton>().Where(x => x is RadioButton);
            foreach (RadioButton radio in radios)
            {
                radio.ForeColor = theme.TextNormal;
            }
        }
        private NotifyIcon notifyIcon1;
        private TableLayoutPanel tableLayoutPanel1;
        private Label commandInsert;
        private Label messageInsert;
        private Button repoPageButton;
        private Label command;
        private Label message;
        private Button copyMessageButton;
        private Button copyCommandButton;
    }
}