using CeNiN;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

/*
 *--------------------------------------------------------------------------
 * CeNiN; a convolutional neural network implementation in pure C#
 * Huseyin Atasoy
 * huseyin @atasoyweb.net
 * http://huseyinatasoy.com
 * March 2019
 *--------------------------------------------------------------------------
 * Copyright 2019 Huseyin Atasoy
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 * http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 *--------------------------------------------------------------------------
 */

namespace CeNiN_Winform
{
    public partial class Form1 : Form
    {
        private DateTime dateTime;
        private CNNOperate cnnoper = new CNNOperate();
        private RefreshUIImage refresh = null;
        private RefreshUIText refreshtxt = null;

        public delegate void RefreshUIImage(byte[] image);

        public delegate void RefreshUIText(string msg);

        public void OnImageLoad(byte[] image)
        {
            this.Invoke(refresh, image);
        }

        public void OnTextLoad(string msg)
        {
            this.Invoke(refreshtxt, msg);
        }

        private CNN cnn;

        public Form1()
        {
#if DEBUG
            MessageBox.Show("需要将trainset文件夹中的数据解压");
#endif
            InitializeComponent();
            LoadInitData();
            MainForm.OnImageReconizeed += ShowImage;
            MainForm.MsgSend += ShowText;
            refresh = new RefreshUIImage(OnImageLoad);
            refreshtxt = new RefreshUIText(OnTextLoad);
        }

        private void LoadInitData()
        {
            string cnnFileLoc = System.Environment.CurrentDirectory + "\\TrainSet\\" + "imagenet-vgg-verydeep-16.cenin";
            cnn = new CNN(cnnFileLoc);
            cnnoper.InitCNN(System.Environment.CurrentDirectory, cnnFileLoc);
            if (cnn != null) { button2.Enabled = true; }
        }
        /// <summary>
        /// load cnn set
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog opf = new OpenFileDialog();
            opf.Filter = "CeNiN 文件|*.cenin";
            if (opf.ShowDialog() != DialogResult.OK) return;
            textBox1.Clear();
            Console.WriteLine("解析 CeNiN 文件...");
            Application.DoEvents();
            tic();
            cnn = new CNN(opf.FileName);
            Console.WriteLine("加载训练集耗时 " + toc() + " 秒.");
            Console.WriteLine(cnn.layerCount + "+2 图层, "
                    + cnn.totalWeightCount + " 权重 和"
                    + cnn.totalBiasCount + " 偏倚 已加载 "
                    + toc() + " 秒.");
            cnnoper.InitCNN(System.Environment.CurrentDirectory, opf.FileName);
            button2.Enabled = true;
        }
        /// <summary>
        /// load image
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog opf = new OpenFileDialog();
            opf.Filter = "图片文件|*.bmp;*.jpeg;*.jpg;*.png";
            if (opf.ShowDialog() != DialogResult.OK) return;
            Bitmap b = new Bitmap(opf.FileName);
            cnn.inputLayer.setInput(b, Input.ResizingMethod.ZeroPad);
            pictureBox1.Image = (Image)cnn.inputLayer.ResizedInputBmp.Clone();
            tic();
            Layer currentLayer = cnn.inputLayer;
            int i = 0;
            while (currentLayer.nextLayer != null)
            {
                if (i == 0)
                    Console.WriteLine("加载位图数据...");
                else
                    Console.WriteLine("图层 " + i + " (" + currentLayer.type + ") ...");

                Application.DoEvents();

                currentLayer.feedNext();
                currentLayer = currentLayer.nextLayer;
                i += 1;
            }

            Output outputLayer = (Output)currentLayer;
            Console.WriteLine("完成，共耗时 in " + toc().ToString() + " 秒");
            string decision = outputLayer.getDecision();
            string hLine = new string('-', 100);
            Console.WriteLine(hLine);
            for (i = 2; i >= 0; i--)
                prependLine(" #" + (i + 1) + "   " + outputLayer.sortedClasses[i] + " (" + Math.Round(outputLayer.probabilities[i], 3) + ")");
            Console.WriteLine(hLine);
            Console.WriteLine("最可能是如下三种: ");
            Console.WriteLine(hLine);
            prependLine("描述: " + decision);
            Console.WriteLine(hLine);
        }

        private DateTime tic()
        {
            dateTime = DateTime.Now;
            return dateTime;
        }

        private double toc()
        {
            return Math.Round((DateTime.Now - dateTime).TotalSeconds, 3);
        }

        private void prependLine(string text)
        {
            textBox1.Text = text + "\r\n" + textBox1.Text;
        }
        /// <summary>
        /// stop operate
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button5_Click(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// 退出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button6_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        /// <summary>
        /// 开始处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button4_Click(object sender, EventArgs e)
        {
            var fileList = FileOperate.GetAllFiles(cnnoper.folder, "*.bmp;*.jpeg;*.jpg;*.png");
            cnnoper.MassOperateFiles(fileList, cnnoper.folder);
        }

        /// <summary>
        /// select folder
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.Description = "请选择图片文件路径";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                cnnoper.InitCNN(dialog.SelectedPath);
            }
        }

        public void ShowImage(byte[] fileBytes)
        {
            pictureBox1.Image = Image.FromStream(new MemoryStream(fileBytes));
        }

        public void ShowText(string msg)
        {
            textBox1.Text = msg;
        }
    }
}