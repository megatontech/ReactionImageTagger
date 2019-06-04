using CeNiN;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CeNiN_Winform
{
    public class CNNOperate
    {
        private CNN cnn;
        public string folder;

        public void InitCNN(string folderStr, string ceninFile = "")
        {
            string cnnFileLoc = System.Environment.CurrentDirectory + "\\TrainSet\\" + "imagenet-vgg-verydeep-16.cenin";
            if (string.IsNullOrEmpty(ceninFile))
            {
                cnnFileLoc = System.Environment.CurrentDirectory + "\\TrainSet\\" + "imagenet-vgg-verydeep-16.cenin";
            }
            else { cnnFileLoc = ceninFile; }
            cnn = new CNN(cnnFileLoc);
            folder = folderStr;
        }

        public void MassOperateFiles(List<string> fileList, string destFolder)
        {
            MainForm form = new MainForm();
            #region Task
            Task task = Task.Factory.StartNew(() => {
                foreach (string item in fileList)
                {
                    try
                    {
                        //cnn
                        Bitmap b = new Bitmap(item);
                        cnn.inputLayer.setInput(b, Input.ResizingMethod.ZeroPad);

                        Layer currentLayer = cnn.inputLayer;
                        int i = 0;
                        while (currentLayer.nextLayer != null)
                        {
                            currentLayer.feedNext();
                            currentLayer = currentLayer.nextLayer;
                            i += 1;
                        }
                        Output outputLayer = (Output)currentLayer;
                        string decision = outputLayer.getDecision();
                        string imageTagStr = decision;
                        decision = decision.Split(',')[0];
                        string pattern = "[A-Za-z0-9]";
                        string strRet = "";
                        MatchCollection results = Regex.Matches(decision, pattern);
                        foreach (var v in results)
                        {
                            strRet += v.ToString();
                        }
                        decision = strRet;
                        FileStream fileStream = new FileStream(item, FileMode.Open, FileAccess.Read);
                        int byteLength = (int)fileStream.Length;
                        byte[] fileBytes = new byte[byteLength];
                        fileStream.Read(fileBytes, 0, byteLength);
                        fileStream.Close();
                        MainForm.ImageLoad(fileBytes);
                        MainForm.MessageSend(decision);
                        b.Dispose();
                        //add tag
                        //for (i = 2; i >= 0; i--) { imageTagStr += outputLayer.sortedClasses[i]+"|"; }
                        FileOperate.AddTagToImage(item, imageTagStr);
                        //create folder if not exist
                        FileOperate.CreateFolder(destFolder, decision);
                        //move file
                        FileOperate.MoveFile(destFolder + "\\" + decision, item);
                    }
                    catch (System.Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }
            });
            #endregion

            #region Parallel

            //Parallel.ForEach(fileList, item =>
            //{
            //    try
            //    {
            //        //cnn
            //        using (Bitmap b = new Bitmap(item))
            //        {
            //            cnn.inputLayer.setInput(b, Input.ResizingMethod.ZeroPad);
            //            Layer currentLayer = cnn.inputLayer;
            //            int i = 0;
            //            while (currentLayer.nextLayer != null)
            //            {
            //                currentLayer.feedNext();
            //                currentLayer = currentLayer.nextLayer;
            //                i += 1;
            //            }
            //            Output outputLayer = (Output)currentLayer;
            //            string decision = outputLayer.getDecision();
            //            string imageTagStr = decision;
            //            decision = decision.Split(',')[0];
            //            string pattern = "[A-Za-z0-9]";
            //            string strRet = "";
            //            MatchCollection results = Regex.Matches(decision, pattern);
            //            foreach (var v in results)
            //            {
            //                strRet += v.ToString();
            //            }
            //            decision = strRet;
            //            FileStream fileStream = new FileStream(item, FileMode.Open, FileAccess.Read);
            //            int byteLength = (int)fileStream.Length;
            //            byte[] fileBytes = new byte[byteLength];
            //            fileStream.Read(fileBytes, 0, byteLength);
            //            fileStream.Close();
            //            MainForm.ImageLoad(fileBytes);
            //            //add tag
            //            for (i = 2; i >= 0; i--) { imageTagStr += outputLayer.sortedClasses[i] + "|"; }
            //            FileOperate.AddTagToImage(item, imageTagStr);
            //            MainForm.MessageSend(imageTagStr);
            //            //create folder if not exist
            //            FileOperate.CreateFolder(destFolder, decision);
            //            //move file
            //            FileOperate.MoveFile(destFolder + "\\" + decision, item);
            //        }
            //    }
            //    catch (System.Exception e)
            //    {
            //        Console.WriteLine(e.Message);
            //    }
            //});
            #endregion
            #region foreach

            //foreach (string item in fileList)
            //{
            //    try
            //    {
            //        //cnn
            //        Bitmap b = new Bitmap(item);
            //        cnn.inputLayer.setInput(b, Input.ResizingMethod.ZeroPad);

            //        Layer currentLayer = cnn.inputLayer;
            //        int i = 0;
            //        while (currentLayer.nextLayer != null)
            //        {
            //            currentLayer.feedNext();
            //            currentLayer = currentLayer.nextLayer;
            //            i += 1;
            //        }
            //        Output outputLayer = (Output)currentLayer;
            //        string decision = outputLayer.getDecision();
            //        string imageTagStr = decision;
            //        decision = decision.Split(',')[0];
            //        string pattern = "[A-Za-z0-9]";
            //        string strRet = "";
            //        MatchCollection results = Regex.Matches(decision, pattern);
            //        foreach (var v in results)
            //        {
            //            strRet += v.ToString();
            //        }
            //        decision = strRet;
            //        FileStream fileStream = new FileStream(item, FileMode.Open, FileAccess.Read);
            //        int byteLength = (int)fileStream.Length;
            //        byte[] fileBytes = new byte[byteLength];
            //        fileStream.Read(fileBytes, 0, byteLength);
            //        fileStream.Close();
            //        MainForm.ImageLoad(fileBytes);
            //        MainForm.MessageSend(decision);
            //        b.Dispose();
            //        //add tag
            //        //for (i = 2; i >= 0; i--) { imageTagStr += outputLayer.sortedClasses[i]+"|"; }
            //        FileOperate.AddTagToImage(item, imageTagStr);
            //        //create folder if not exist
            //        FileOperate.CreateFolder(destFolder, decision);
            //        //move file
            //        FileOperate.MoveFile(destFolder + "\\" + decision, item);
            //    }
            //    catch (System.Exception e)
            //    {
            //        Console.WriteLine(e.Message);
            //    }
            //}
            #endregion

        }
    }
}