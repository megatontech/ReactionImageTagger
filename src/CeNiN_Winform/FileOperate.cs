using System.Collections.Generic;
using System.IO;

namespace CeNiN_Winform
{
    public static class FileOperate
    {
        public static void CreateFolder(string desc, string name)
        {
            Directory.CreateDirectory(desc + "\\" + name);
        }

        public static void MoveFile(string desc, string oriname)
        {
            string fileName = "";
            fileName = Path.GetFileName(oriname);
            Directory.Move(oriname, desc + "\\" + fileName);
        }

        /// <summary>
        /// 获取指定目录下对应扩展名的文件
        /// </summary>
        /// <param name="folder"></param>
        /// <param name="extNameList"></param>
        /// <returns></returns>
        public static List<string> GetAllFiles(string folder, string extNameList)
        {
            List<string> fileList = new List<string>();
            DirectoryInfo fdir = new DirectoryInfo(folder);
            FileInfo[] file = fdir.GetFiles();
            if (file.Length != 0) //当前目录文件或文件夹不为空
            {
                foreach (FileInfo f in file) //显示当前目录所有文件
                {
                    if (extNameList.ToLower().IndexOf(f.Extension.ToLower()) >= 0)
                    {
                        fileList.Add(f.FullName);
                    }
                }
            }
            return fileList;
        }

        public static void LockFolder(string folder)
        {
        }

        public static void AddTagToImage(string image, string tagStr)
        {
            //try
            //{
            //    System.Diagnostics.Process p = new System.Diagnostics.Process();
            //    p.StartInfo.FileName = "regsvr32";
            //    p.StartInfo.Arguments = @"/s D:\竞界科技\CeNiN\src\CeNiN_Winform\obj\Debug\Interop.DSOFile.dll";
            //    p.Start();
            //    p.WaitForExit();
            //    p.Close();
            //    p.Dispose();
            //}
            //catch (System.Exception)
            //{
            //}

            //DSOFile.OleDocumentProperties dso = new DSOFile.OleDocumentProperties();
            //dso.Open(image, false, DSOFile.dsoFileOpenOptions.dsoOptionOpenReadOnlyIfNoWriteAccess);
            //dso.SummaryProperties.Comments = tagStr;
            //dso.SummaryProperties.Keywords = tagStr;
            //dso.Save();
            //dso.Close(false);

            //SetProperty(image, tagStr, SummaryPropId.Comments);
            //var file = image;
            //var shell = new ShellClass();
            //var dir = shell.NameSpace(Path.GetDirectoryName(file));
            //var item = dir.ParseName(Path.GetFileName(file));
            //var remark = dir.GetDetailsOf(item, 14);// https://bbs.csdn.net/topics/290026604
            //dir.ParseName
            //https://www.cnblogs.com/chenyangsocool/p/7511161.html
            //OleDocumentProperties file = new OleDocumentProperties();//创建一个DSOFile对象
            //file.Open(image,false, dsoFileOpenOptions.dsoOptionDefault);
            //file.SummaryProperties.Author = tagStr;
            //file.SummaryProperties.Comments = tagStr;
            //file.SummaryProperties.Subject = tagStr;
            //file.Save();
            //file.Close();
            //https://stackoverflow.com/questions/46420695/c-sharp-add-extra-property-info-to-a-image
            //https://stackoverflow.com/questions/37755564/how-to-save-image-with-properties-in-c-sharp
            //https://www.codeproject.com/Articles/3615/File-Information-using-C
            //Image img = Image.FromFile(image);
            //System.Drawing.Imaging.PropertyItem prop = img.PropertyItems[0];

            //SetProperty(ref prop, 33432, "Test Info...");
            //img.SetPropertyItem(prop);
            //img.Save(image);
            //var storage = new CompoundStorage(image, false); // open for write
            //storage.Properties.Comments = tagStr; // change well-known "comments" property
            //storage.Properties.Category = tagStr; // change well-known "comments" property
            //storage.Properties.Company = "megatontech"; // change well-known "comments" property
            //storage.CommitChanges();
        }

        ////        public static void SetProperty(string filename, string msg, SummaryPropId summaryType)
        ////        {
        ////            // first you need to either create or open a file and its
        ////            // property set stream
        ////            //申明接口(指针)
        ////            IPropertySetStorage propSetStorage = null;
        ////            //com 组件的 clsid 参见IPropertySetStorage定义
        ////            Guid IID_PropertySetStorage = new
        ////Guid("0000013A-0000-0000-C000-000000000046");

        ////            //Applications written for Windows 2000, Windows Server 2003 and Windows XP must use StgCreateStorageEx rather than StgCreateDocfile to take advantage of the enhanced Windows 2000 and Windows XP Structured Storage features
        ////            uint hresult = ole32.StgOpenStorageEx(
        ////                filename,
        ////                (int)(STGM.SHARE_EXCLUSIVE | STGM.READWRITE),
        ////                (int)STGFMT.FILE,
        ////                0,
        ////                (IntPtr)0,
        ////                (IntPtr)0,
        ////                ref IID_PropertySetStorage,
        ////                ref propSetStorage); //返回指针

        ////            // next you need to create or open the Summary Information property set
        ////            Guid fmtid_SummaryProperties = new
        ////Guid("F29F85E0-4FF9-1068-AB91-08002B27B3D9");
        ////            IPropertyStorage propStorage = null;

        ////            hresult = propSetStorage.Create(
        ////                ref fmtid_SummaryProperties,
        ////                (IntPtr)0,
        ////                (int)PROPSETFLAG.DEFAULT,
        ////                (int)(STGM.CREATE | STGM.READWRITE |
        ////STGM.SHARE_EXCLUSIVE),
        ////                ref propStorage);

        ////            // next, you assemble a property descriptor for the property you
        ////            // want to write to, in our case the Comment property
        ////            PropSpec propertySpecification = new PropSpec();
        ////            propertySpecification.ulKind = 1;
        ////            propertySpecification.Name_Or_ID = new
        ////IntPtr((int)summaryType);

        ////            //now, set the value you want in a property variant
        ////            PropVariant propertyValue = new PropVariant();
        ////            propertyValue.FromObject(msg);

        ////            // Simply pass the property spec and its new value to the WriteMultiple
        ////            // method and you're almost done
        ////            propStorage.WriteMultiple(1, ref propertySpecification, ref
        ////propertyValue, 2);

        ////            // the only thing left to do is commit your changes.  Now you're done!
        ////            hresult = propStorage.Commit((int)STGC.DEFAULT);

        ////            //下面的很关键,如何关闭一个非托管的指针,如果不关闭,则本程序不关闭,文件被锁定!
        ////            System.Runtime.InteropServices.Marshal.ReleaseComObject(propSetStorage);
        ////            propSetStorage = null;
        ////            GC.Collect();
        ////        }
    }

    //    public enum SummaryPropId : int
    //    {
    //        Title = 0x00000002,
    //        Subject = 0x00000003,
    //        Author = 0x00000004,
    //        Keywords = 0x00000005,
    //        Comments = 0x00000006,
    //        Template = 0x00000007,
    //        LastSavedBy = 0x00000008,
    //        RevisionNumber = 0x00000009,
    //        TotalEditingTime = 0x0000000A,
    //        LastPrinted = 0x0000000B,
    //        CreateDateTime = 0x0000000C,
    //        LastSaveDateTime = 0x0000000D,
    //        NumPages = 0x0000000E,
    //        NumWords = 0x0000000F,
    //        NumChars = 0x00000010,
    //        Thumbnail = 0x00000011,
    //        AppName = 0x00000012,
    //        Security = 0x00000013
    //    }

    //    public enum STGC : int
    //    {
    //        DEFAULT = 0,
    //        OVERWRITE = 1,
    //        ONLYIFCURRENT = 2,
    //        DANGEROUSLYCOMMITMERELYTODISKCACHE = 4,
    //        CONSOLIDATE = 8
    //    }

    //    public enum PROPSETFLAG : int
    //    {
    //        DEFAULT = 0,
    //        NONSIMPLE = 1,
    //        ANSI = 2,
    //        UNBUFFERED = 4,
    //        CASE_SENSITIVE = 8
    //    }

    //    public enum STGM : int
    //    {
    //        READ = 0x00000000,
    //        WRITE = 0x00000001,
    //        READWRITE = 0x00000002,
    //        SHARE_DENY_NONE = 0x00000040,
    //        SHARE_DENY_READ = 0x00000030,
    //        SHARE_DENY_WRITE = 0x00000020,
    //        SHARE_EXCLUSIVE = 0x00000010,
    //        PRIORITY = 0x00040000,
    //        CREATE = 0x00001000,
    //        CONVERT = 0x00020000,
    //        FAILIFTHERE = 0x00000000,
    //        DIRECT = 0x00000000,
    //        TRANSACTED = 0x00010000,
    //        NOSCRATCH = 0x00100000,
    //        NOSNAPSHOT = 0x00200000,
    //        SIMPLE = 0x08000000,
    //        DIRECT_SWMR = 0x00400000,
    //        DELETEONRELEASE = 0x04000000
    //    }

    //    public enum STGFMT : int
    //    {
    //        STORAGE = 0,
    //        FILE = 3,
    //        ANY = 4,
    //        DOCFILE = 5
    //    }

    //    [StructLayout(LayoutKind.Explicit, Size = 8, CharSet = CharSet.Unicode)]
    //    public struct PropSpec
    //    {
    //        [FieldOffset(0)]
    //        public int ulKind;

    //        [FieldOffset(4)]
    //        public IntPtr Name_Or_ID;
    //    }

    //    [StructLayout(LayoutKind.Explicit, Size = 16)]
    //    public struct PropVariant
    //    {
    //        [FieldOffset(0)]
    //        public short variantType;

    //        [FieldOffset(8)]
    //        public IntPtr pointerValue;

    //        [FieldOffset(8)]
    //        public byte byteValue;

    //        [FieldOffset(8)]
    //        public long longValue;

    //        public void FromObject(object obj)
    //        {
    //            if (obj.GetType() == typeof(string))
    //            {
    //                this.variantType = (short)VarEnum.VT_LPWSTR;
    //                this.pointerValue = Marshal.StringToHGlobalUni((string)obj);
    //            }
    //        }
    //    }

    //    [ComVisible(true), ComImport(),
    //    Guid("0000013A-0000-0000-C000-000000000046"),
    //    InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    //    public interface IPropertySetStorage
    //    {
    //        uint Create(
    //            [In, MarshalAs(UnmanagedType.Struct)] ref System.Guid rfmtid,
    //      [In] IntPtr pclsid,
    //      [In] int grfFlags,
    //            [In] int grfMode,
    //      ref IPropertyStorage propertyStorage);

    //        int Open(
    //            [In, MarshalAs(UnmanagedType.Struct)] ref System.Guid rfmtid,
    //            [In] int grfMode,
    //                  [Out] IPropertyStorage propertyStorage);
    //    }

    //    [ComVisible(true), ComImport(),
    //    Guid("00000138-0000-0000-C000-000000000046"),
    //    InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    //    public interface IPropertyStorage
    //    {
    //        int ReadMultiple(
    //            uint numProperties,
    //            PropSpec[] propertySpecifications,
    //            PropVariant[] propertyValues);

    //        int WriteMultiple(
    //            uint numProperties,
    //            [MarshalAs(UnmanagedType.Struct)] ref PropSpec
    //propertySpecification,
    //            ref PropVariant propertyValues,
    //            int propIDNameFirst);

    //        uint Commit(
    //            int commitFlags);
    //    }

    //    public enum HResults : uint
    //    {
    //        S_OK = 0,
    //        STG_E_FILEALREADYEXISTS = 0x80030050
    //    }

    //    public class ole32
    //    {
    //        [StructLayout(LayoutKind.Explicit, Size = 12,
    //CharSet = CharSet.Unicode)]
    //        public struct STGOptions
    //        {
    //            [FieldOffset(0)]
    //            private ushort usVersion;

    //            [FieldOffset(2)]
    //            private ushort reserved;

    //            [FieldOffset(4)]
    //            private uint uiSectorSize;

    //            [FieldOffset(8), MarshalAs(UnmanagedType.LPWStr)]
    //            private string
    //pwcsTemplateFile;
    //        }

    //        [DllImport("ole32.dll", CharSet = CharSet.Unicode)]
    //        public static extern uint StgCreateStorageEx(
    //            [MarshalAs(UnmanagedType.LPWStr)] string name,
    //            int accessMode, int storageFileFormat, int fileBuffering,
    //            IntPtr options, IntPtr reserved, ref System.Guid riid,
    //            [MarshalAs(UnmanagedType.Interface)] ref IPropertySetStorage
    //propertySetStorage);

    //        [DllImport("ole32.dll", CharSet = CharSet.Unicode)]
    //        public static extern uint StgOpenStorageEx(
    //            [MarshalAs(UnmanagedType.LPWStr)] string name,
    //            int accessMode, int storageFileFormat, int fileBuffering,
    //            IntPtr options, IntPtr reserved, ref System.Guid riid,
    //            [MarshalAs(UnmanagedType.Interface)] ref IPropertySetStorage
    //propertySetStorage);
    //    }
}