using System;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Moamam.Lib
{
    public class FileUploader
    {
        #region FileUpload()
        /// <summary>
        /// 파일을 업로드 한다.
        /// </summary>
        /// <param name="fileUpload">파일업로드 서버컨트롤</param>
        /// <param name="saveFolder">파일저장폴더경로(물리적경로)</param>
        /// <returns>실제 저장된 파일경로롤 리턴한다</returns>
        static public string FileUpload(FileUpload fileUpload, string saveFolder, bool overwrite)
        {
            return FileUpload(fileUpload, saveFolder, null, null, null, overwrite);
        }

        static public string FileUpload(FileUpload fileUpload, string saveFolder, string prefix, string suffix)
        {
            return FileUpload(fileUpload, saveFolder, prefix, suffix, null, false);
        }

        /// <summary>
        /// 파일을 업로드 한다.
        /// </summary>
        /// <param name="fileUpload">파일업로드 서버컨트롤</param>
        /// <param name="saveFolder">파일저장폴더경로(물리적경로)</param>
        /// <param name="prefix">저장될 파일명 앞에 붙는 문자열</param>
        /// <param name="suffix">저장될 파일명 뒤에 붙는 문자열</param>
        /// <param name="allowedExtensions">저장이 허용되는 파일확장자 문자배열</param>
        /// <returns>실제 저장된 파일경로롤 리턴한다</returns>
        static public string FileUpload(FileUpload fileUpload, string saveFolder, string prefix, string suffix, string[] allowedExtensions, bool overwrite)
        {
            Boolean fileOK = false;
            String path = saveFolder;
            if (fileUpload.HasFile)
            {
                //확장자 체크(allowedExtensions ==> (ex) ".xls",".dat",...
                if (allowedExtensions != null && allowedExtensions.Length > 0)
                {
                    String fileExtension = Path.GetExtension(fileUpload.FileName).ToLower();
                    for (int i = 0; i < allowedExtensions.Length; i++)
                    {
                        if (fileExtension == allowedExtensions[i])
                            fileOK = true;
                    }
                }
                else//확장자 체크 안함
                {
                    fileOK = true;
                }
            }

            if (fileOK)
            {
                try
                {
                    string onlyFileName = Path.GetFileNameWithoutExtension(fileUpload.FileName);
                    string fileExtension = Path.GetExtension(fileUpload.FileName);

                    //prefix 및 suffix 체크
                    if (prefix != null && prefix != "")
                        onlyFileName = prefix + onlyFileName;
                    if (suffix != null && suffix != "")
                        onlyFileName = onlyFileName + suffix;

                    string fileName = Path.Combine(path, onlyFileName + fileExtension);

                    if (!overwrite)
                    {
                        if (File.Exists(fileName))
                            throw new Exception("이미 존재하는 파일명입니다. 파일명을 변경하세요.");
                    }

                    fileUpload.PostedFile.SaveAs(fileName);
                    return fileName;
                }
                catch
                {
                    throw;
                }
            }
            else
                throw new Exception("Error : Cannot accept this file type!");
        }
        #endregion
    }
}
