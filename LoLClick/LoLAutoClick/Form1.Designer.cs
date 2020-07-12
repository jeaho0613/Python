namespace LoLAutoClick
{
    partial class Form1
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.label_posX = new System.Windows.Forms.Label();
            this.label_posY = new System.Windows.Forms.Label();
            this.text_posX = new System.Windows.Forms.TextBox();
            this.text_posY = new System.Windows.Forms.TextBox();
            this.Bnt_Search = new System.Windows.Forms.Button();
            this.Bnt_PosChange = new System.Windows.Forms.Button();
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.Bnt_Exit = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // label_posX
            // 
            this.label_posX.AutoSize = true;
            this.label_posX.Location = new System.Drawing.Point(13, 26);
            this.label_posX.Name = "label_posX";
            this.label_posX.Size = new System.Drawing.Size(38, 12);
            this.label_posX.TabIndex = 0;
            this.label_posX.Text = "label1";
            // 
            // label_posY
            // 
            this.label_posY.AutoSize = true;
            this.label_posY.Location = new System.Drawing.Point(153, 25);
            this.label_posY.Name = "label_posY";
            this.label_posY.Size = new System.Drawing.Size(38, 12);
            this.label_posY.TabIndex = 1;
            this.label_posY.Text = "label2";
            // 
            // text_posX
            // 
            this.text_posX.Location = new System.Drawing.Point(15, 41);
            this.text_posX.Name = "text_posX";
            this.text_posX.Size = new System.Drawing.Size(100, 21);
            this.text_posX.TabIndex = 2;
            // 
            // text_posY
            // 
            this.text_posY.Location = new System.Drawing.Point(155, 40);
            this.text_posY.Name = "text_posY";
            this.text_posY.Size = new System.Drawing.Size(100, 21);
            this.text_posY.TabIndex = 3;
            // 
            // Bnt_Search
            // 
            this.Bnt_Search.Location = new System.Drawing.Point(15, 77);
            this.Bnt_Search.Name = "Bnt_Search";
            this.Bnt_Search.Size = new System.Drawing.Size(75, 23);
            this.Bnt_Search.TabIndex = 4;
            this.Bnt_Search.Text = "검색";
            this.Bnt_Search.UseVisualStyleBackColor = true;
            this.Bnt_Search.Click += new System.EventHandler(this.Bnt_Search_Click);
            // 
            // Bnt_PosChange
            // 
            this.Bnt_PosChange.Location = new System.Drawing.Point(155, 77);
            this.Bnt_PosChange.Name = "Bnt_PosChange";
            this.Bnt_PosChange.Size = new System.Drawing.Size(75, 23);
            this.Bnt_PosChange.TabIndex = 5;
            this.Bnt_PosChange.Text = "커서변경";
            this.Bnt_PosChange.UseVisualStyleBackColor = true;
            this.Bnt_PosChange.Click += new System.EventHandler(this.Bnt_PosChange_Click);
            // 
            // timer2
            // 
            this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
            // 
            // Bnt_Exit
            // 
            this.Bnt_Exit.Location = new System.Drawing.Point(213, 128);
            this.Bnt_Exit.Name = "Bnt_Exit";
            this.Bnt_Exit.Size = new System.Drawing.Size(75, 23);
            this.Bnt_Exit.TabIndex = 6;
            this.Bnt_Exit.Text = "종료";
            this.Bnt_Exit.UseVisualStyleBackColor = true;
            this.Bnt_Exit.Click += new System.EventHandler(this.Bnt_Exit_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(300, 163);
            this.Controls.Add(this.Bnt_Exit);
            this.Controls.Add(this.Bnt_PosChange);
            this.Controls.Add(this.Bnt_Search);
            this.Controls.Add(this.text_posY);
            this.Controls.Add(this.text_posX);
            this.Controls.Add(this.label_posY);
            this.Controls.Add(this.label_posX);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label label_posX;
        private System.Windows.Forms.Label label_posY;
        private System.Windows.Forms.TextBox text_posX;
        private System.Windows.Forms.TextBox text_posY;
        private System.Windows.Forms.Button Bnt_Search;
        private System.Windows.Forms.Button Bnt_PosChange;
        private System.Windows.Forms.Timer timer2;
        private System.Windows.Forms.Button Bnt_Exit;
    }
}

