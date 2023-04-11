using System;
using System.Drawing;
using ArcFaceSDK.SDKModels;
using ArcFaceSDK.Entity;
using ArcFaceSDK;
using ArcFaceService.Entity;
using System.Threading;
using System.Collections.Generic;
using System.Collections;
using System.Linq;

namespace ArcFaceService.Utils
{
    public class FaceUtil
    {
        private static FaceUtil _instance;
        private FaceEngine faceEngine;

        public static FaceUtil Instance
        {
            get
            {
                return _instance == null ? _instance = new FaceUtil() : _instance;
            }
        }

        public void init(FaceEngine faceEngine)
        {
            this.faceEngine = faceEngine;
        }

        /// <summary>
        /// Trích xuất đặc điểm khuôn mặt
        /// </summary>
        /// <param name="multiFaceInfo"></param>
        /// <returns></returns>
        public List<FaceFeatureUnit> FaceFeatures(Image srcImage, FaceEngine faceEngine, bool fullFeature = true)
        {
            FeatureUnit featureSDK = extractFeature(srcImage, fullFeature, faceEngine);
            if (featureSDK == null)
            {
                throw new Exception("Face feature is null");
            }

            FaceFeatureUnit faceFeatureUnit = new FaceFeatureUnit();
            faceFeatureUnit.quality = featureSDK.quality;
            faceFeatureUnit.age = featureSDK.age;
            faceFeatureUnit.gender = featureSDK.gender;
            faceFeatureUnit.mask = featureSDK.mask;
            faceFeatureUnit.liveness = featureSDK.liveness;
            faceFeatureUnit.leftEyeClose = featureSDK.leftEyeClose;
            faceFeatureUnit.rightEyeClose = featureSDK.rightEyeClose;
            faceFeatureUnit.wearGlass = featureSDK.wearGlass;
            faceFeatureUnit.orient = featureSDK.orient;
            faceFeatureUnit.angle3D = featureSDK.angle3D;
            faceFeatureUnit.faceRect = featureSDK.faceRect;
            faceFeatureUnit.landmark = featureSDK.landmark;
            faceFeatureUnit.feature = featureSDK.feature;

            List<FaceFeatureUnit> lsFaceFeatureUnit = new List<FaceFeatureUnit>();
            lsFaceFeatureUnit.Add(faceFeatureUnit);

            return lsFaceFeatureUnit;
        }

        /// <summary>
        /// Trích xuất đặc điểm khuôn mặt
        /// </summary>
        /// <param name="multiFaceInfo"></param>
        /// <returns></returns>
        public List<FaceFeatureUnit> FaceFeaturesNew(Image srcImage, EngineModel engineModel, bool fullFeature = true)
        {
            FeatureUnit featureSDK = extractFeatureNew(srcImage, fullFeature, engineModel);
            if (featureSDK == null)
            {
                throw new Exception("Face feature is null");
            }

            FaceFeatureUnit faceFeatureUnit = new FaceFeatureUnit();
            faceFeatureUnit.quality = featureSDK.quality;
            faceFeatureUnit.age = featureSDK.age;
            faceFeatureUnit.gender = featureSDK.gender;
            faceFeatureUnit.mask = featureSDK.mask;
            faceFeatureUnit.liveness = featureSDK.liveness;
            faceFeatureUnit.leftEyeClose = featureSDK.leftEyeClose;
            faceFeatureUnit.rightEyeClose = featureSDK.rightEyeClose;
            faceFeatureUnit.wearGlass = featureSDK.wearGlass;
            faceFeatureUnit.orient = featureSDK.orient;
            faceFeatureUnit.angle3D = featureSDK.angle3D;
            faceFeatureUnit.faceRect = featureSDK.faceRect;
            faceFeatureUnit.landmark = featureSDK.landmark;
            faceFeatureUnit.feature = featureSDK.feature;

            List<FaceFeatureUnit> lsFaceFeatureUnit = new List<FaceFeatureUnit>();
            lsFaceFeatureUnit.Add(faceFeatureUnit);

            return lsFaceFeatureUnit;
        }

        public static FeatureUnit extractFeature(Image image, bool extractAll, FaceEngine faceEngine)
        {
            FeatureUnit faceUnit = null;
            EngineModel engineProcess = faceEngine.getEngineProcess();
            MultiFaceInfo multiFaceInfo = faceEngine.ASFDetectFaces(engineProcess, image, ASF_ImagePixelFormat.ASVL_PAF_RGB24_B8G8R8, ASF_DetectModel.ASF_DETECT_MODEL_RGB, extractAll);
            for (int i = 0; i < multiFaceInfo.faceNum; i++)
            {
                if (!extractAll)
                {
                    //Chỉ trích xuất face feature
                    faceUnit = ExtractOnlyFeature(engineProcess, faceEngine, image, multiFaceInfo);
                }
                else
                {
                    //Trích xuất tất cả các thuộc tính
                    faceUnit = ExtractAllFeature(engineProcess, faceEngine, image, multiFaceInfo);
                }
            }

            if (engineProcess != null)
            {
                faceEngine.releaseEngine(engineProcess);
            }

            return faceUnit;
        }

        public FeatureUnit extractFeatureNew(Image image, bool extractAll, EngineModel engineProcess)
        {
            FeatureUnit faceUnit = null;
            MultiFaceInfo multiFaceInfo = faceEngine.ASFDetectFaces(engineProcess, image, ASF_ImagePixelFormat.ASVL_PAF_RGB24_B8G8R8, ASF_DetectModel.ASF_DETECT_MODEL_RGB, extractAll);
            for (int i = 0; i < multiFaceInfo.faceNum; i++)
            {
                if (!extractAll)
                {
                    //Chỉ trích xuất face feature
                    faceUnit = ExtractOnlyFeature(engineProcess, faceEngine, image, multiFaceInfo);
                }
                else
                {
                    //Trích xuất tất cả các thuộc tính
                    faceUnit = ExtractAllFeature(engineProcess, faceEngine, image, multiFaceInfo);
                }
            }

            return faceUnit;
        }

        /// <summary>
        /// Trích xuất các đặc điểm trên khuôn mặt
        /// </summary>
        /// <param name="pEngine">Engine</param>
        /// <param name="image">Image</param>
        /// <param name="thresold">Ngưỡng phát hiện chất lượng hình ảnh</param>
        /// <returns>Con trỏ</returns>
        private static FeatureUnit ExtractOnlyFeature(EngineModel engineModel, FaceEngine faceEngine, Image image, MultiFaceInfo multiFaceInfo, int faceIndex = 0)
        {
            FeatureUnit faceFeatureUnit = new FeatureUnit();

            try
            {
                //Phát hiện khấu trang
                //MaskInfo maskInfo = maskEstimation(engineModel, faceEngine, image, multiFaceInfo);
                //bool isMask = maskInfo.maskArray[0].Equals(1);
                bool isMask = false;

                //Trích xuất đặc điểm
                FaceFeature faceFeature = extractFeature(engineModel, faceEngine, image, multiFaceInfo, faceIndex, isMask);

                faceFeatureUnit.feature = faceFeature.feature;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return faceFeatureUnit;
        }

        /// <summary>
        /// Trích xuất các đặc điểm trên khuôn mặt
        /// </summary>
        /// <param name="pEngine">Engine</param>
        /// <param name="image">Image</param>
        /// <param name="thresold">Ngưỡng phát hiện chất lượng hình ảnh</param>
        /// <returns>Con trỏ</returns>
        private static FeatureUnit ExtractAllFeature(EngineModel engineModel, FaceEngine faceEngine, Image image, MultiFaceInfo multiFaceInfo, int faceIndex = 0)
        {
            FeatureUnit faceFeatureUnit = new FeatureUnit();

            try
            {
                //Độ tuổi
                AgeInfo ageInfo = ageEstimation(engineModel, faceEngine, image, multiFaceInfo);

                //Giới tính
                GenderInfo genderInfo = genderEstimation(engineModel, faceEngine, image, multiFaceInfo);

                //3DAngle
                Face3DAngle face3DAngle = face3DAngleDetection(engineModel, faceEngine, image, multiFaceInfo);

                //Landmark
                //LandMarkInfo landMarkInfo = landMarkEstimation(engineModel, faceEngine, image, multiFaceInfo);

                //Liveness                
                LivenessInfo livenessInfo = livenessInfo_RGB(engineModel, faceEngine, image, multiFaceInfo);

                //Phát hiện khấu trang
                MaskInfo maskInfo = maskEstimation(engineModel, faceEngine, image, multiFaceInfo);
                bool isMask = maskInfo.maskArray[0].Equals(1);

                //Kiểm tra chất lượng hình ảnh
                float quality = extractImageQuality(engineModel, faceEngine, image, multiFaceInfo, faceIndex, isMask);

                //Trích xuất đặc điểm
                FaceFeature faceFeature = extractFeature(engineModel, faceEngine, image, multiFaceInfo, faceIndex, false);

                faceFeatureUnit.quality = quality;
                faceFeatureUnit.age = ageInfo.ageArray[0];
                faceFeatureUnit.gender = genderInfo.genderArray[0];
                faceFeatureUnit.mask = maskInfo.maskArray[0];
                faceFeatureUnit.liveness = livenessInfo.isLive[0];
                faceFeatureUnit.leftEyeClose = multiFaceInfo.leftEyeClosed[0];
                faceFeatureUnit.rightEyeClose = multiFaceInfo.rightEyeClosed[0];
                faceFeatureUnit.wearGlass = multiFaceInfo.wearGlasses[0] >= 0.5 ? 1 : 0;
                faceFeatureUnit.orient = multiFaceInfo.faceOrients[0];
                faceFeatureUnit.angle3D = new float[] { face3DAngle.pitch[0], face3DAngle.roll[0], face3DAngle.yaw[0] };
                faceFeatureUnit.faceRect = multiFaceInfo.faceRects[0];
                //faceFeatureUnit.landmark = landMarkInfo.pointAyy[0];
                faceFeatureUnit.feature = faceFeature.feature;
            }
            catch(Exception ex)
            {
                throw ex;
            }

            return faceFeatureUnit;
        }

        /// <summary>
        /// Trích xuất các đặc điểm khuôn mặt đơn lẻ
        /// </summary>
        /// <param name="pEngine">Engine</param>
        /// <param name="image">Image</param>
        /// <param name="singleFaceInfo"></param>
        /// <returns></returns>
        public static FaceFeature ExtractFeatureSingle(EngineModel engineModel, FaceEngine faceEngine, Image image, SingleFaceInfo singleFaceInfo)
        {
            FaceFeature faceFeature = null;
            try
            {
                MultiFaceInfo multiFaceInfo = new MultiFaceInfo();
                multiFaceInfo.faceNum = 1;
                multiFaceInfo.faceOrients = new int[1];
                multiFaceInfo.faceOrients[0] = singleFaceInfo.faceOrient;
                multiFaceInfo.faceRects = new MRECT[1];
                multiFaceInfo.faceRects[0] = singleFaceInfo.faceRect;
                multiFaceInfo.faceDataInfoList = new FaceDataInfo[1];
                multiFaceInfo.faceDataInfoList[0] = singleFaceInfo.faceDataInfo;

                //Phát hiện khẩu trang
                int retCode = -1;
                MaskInfo maskInfo = maskEstimation(engineModel, faceEngine, image, multiFaceInfo);
                if (retCode != 0 || maskInfo.maskArray == null || maskInfo.maskArray.Length <= 0)
                {
                    return faceFeature;
                }
                bool isMask = maskInfo.maskArray[0].Equals(1);
                faceEngine.ASFFaceFeatureExtractEx(engineModel, image, multiFaceInfo, ASF_RegisterOrNot.ASF_RECOGNITION, out faceFeature, 0, isMask);
            }
            catch (Exception ex)
            {
                LogUtil.LogInfo(typeof(FaceUtil), ex);
            }
            return faceFeature;
        }

        /// <summary>
        /// Trích xuất độ tuổi
        /// </summary>
        /// <param name="pEngine">Engine</param>
        /// <param name="imageInfo"></param>
        /// <param name="multiFaceInfo"></param>
        /// <returns></returns>
        private static AgeInfo ageEstimation(EngineModel workEngine, FaceEngine faceEngine, Image image, MultiFaceInfo multiFaceInfo)
        {
            AgeInfo ageInfo = new AgeInfo();
            int retCode = faceEngine.ASFProcessEx(workEngine, image, multiFaceInfo, FaceEngineMask.ASF_AGE);
            if (retCode == 0)
            {
                retCode = faceEngine.ASFGetAge(workEngine, out ageInfo);
            }

            if (retCode != 0)
            {
                throw new Exception(string.Format("Trích xuất độ tuổi không thành công: {0}", retCode));
            }
            return ageInfo;
        }

        /// <summary>
        /// Trích xuất giới tính
        /// </summary>
        /// <param name="pEngine"></param>
        /// <param name="imageInfo"></param>
        /// <param name="multiFaceInfo"></param>
        /// <returns></returns>
        private static GenderInfo genderEstimation(EngineModel workEngine, FaceEngine faceEngine, Image image, MultiFaceInfo multiFaceInfo)
        {
            GenderInfo genderInfo = new GenderInfo();
            int retCode = faceEngine.ASFProcessEx(workEngine, image, multiFaceInfo, FaceEngineMask.ASF_GENDER);
            if (retCode == 0)
            {
                retCode = faceEngine.ASFGetGender(workEngine, out genderInfo);
            }

            if (retCode != 0)
            {
                throw new Exception(string.Format("Trích xuất giới tính không thành công: {0}", retCode));
            }
            return genderInfo;
        }

        /// <summary>
        /// Trích xuất góc 3D khuôn mặt
        /// </summary>
        /// <param name="pEngine"></param>
        /// <param name="imageInfo"></param>
        /// <param name="multiFaceInfo"></param>
        /// <returns></returns>
        private static Face3DAngle face3DAngleDetection(EngineModel engineModel, FaceEngine faceEngine, Image image, MultiFaceInfo multiFaceInfo)
        {
            Face3DAngle face3DAngle = new Face3DAngle();
            int retCode = faceEngine.ASFProcessEx(engineModel, image, multiFaceInfo, FaceEngineMask.ASF_FACE3DANGLE);
            if (retCode == 0)
            {
                retCode = faceEngine.ASFGetFace3DAngle(engineModel, out face3DAngle);
            }

            if (retCode != 0)
            {
                throw new Exception(string.Format("Trích xuất hướng các góc khuôn mặt không thành công: {0}", retCode));
            }
            return face3DAngle;
        }

        /// <summary>
        /// Trích xuất sự sống bằng ảnh RGB
        /// </summary>
        /// <param name="pEngine"></param>
        /// <param name="imageInfo"></param>
        /// <param name="singleFaceInfo"></param>
        /// <returns></returns>
        private static LivenessInfo LivenessInfo_RGB(EngineModel engineModel, FaceEngine faceEngine, Image image, SingleFaceInfo singleFaceInfo, out int retCode)
        {
            LivenessInfo livenessInfo = new LivenessInfo();
            retCode = -1;
            try
            {
                MultiFaceInfo multiFaceInfo = new MultiFaceInfo();
                multiFaceInfo.faceNum = 1;
                multiFaceInfo.faceOrients = new int[1];
                multiFaceInfo.faceOrients[0] = singleFaceInfo.faceOrient;
                multiFaceInfo.faceRects = new MRECT[1];
                multiFaceInfo.faceRects[0] = singleFaceInfo.faceRect;
                multiFaceInfo.faceDataInfoList = new FaceDataInfo[1];
                multiFaceInfo.faceDataInfoList[0] = singleFaceInfo.faceDataInfo;

                retCode = faceEngine.ASFProcessEx(engineModel, image, multiFaceInfo, FaceEngineMask.ASF_LIVENESS);
                if (retCode == 0 || retCode == 86019)
                {
                    retCode = faceEngine.ASFGetLivenessScore(engineModel, out livenessInfo);
                }
            }
            catch (Exception ex)
            {
                LogUtil.LogInfo(typeof(FaceUtil), ex);
            }
            return livenessInfo;
        }

        /// <summary>
        /// Trích xuất sự sống bằng ảnh RGB
        /// </summary>
        /// <param name="pEngine"></param>
        /// <param name="imageInfo"></param>
        /// <param name="multiFaceInfo"></param>
        /// <returns></returns>        
        private static LivenessInfo livenessInfo_RGB(EngineModel engineModel, FaceEngine faceEngine, Image image, MultiFaceInfo multiFaceInfo)
        {
            LivenessInfo livenessInfo = new LivenessInfo();
            int retCode = faceEngine.ASFProcessEx(engineModel, image, multiFaceInfo, FaceEngineMask.ASF_LIVENESS);
            if (retCode == 0)
            {
                retCode = faceEngine.ASFGetLivenessScore(engineModel, out livenessInfo);
            }

            if (retCode != 0)
            {
                throw new Exception(string.Format("Trích xuất sự sống không thành công: {0}", retCode));
            }
            return livenessInfo;
        }

        /// <summary>
        /// Trích xuất sự sống bằng ảnh IR
        /// </summary>
        /// <param name="pEngine"></param>
        /// <param name="imageInfo"></param>
        /// <param name="singleFaceInfo"></param>
        /// <returns></returns>
        private static LivenessInfo livenessInfo_IR(EngineModel engineModel, FaceEngine faceEngine, Image image, SingleFaceInfo singleFaceInfo, out int retCode)
        {
            LivenessInfo livenessInfo = new LivenessInfo();
            retCode = -1;
            try
            {
                MultiFaceInfo multiFaceInfo = new MultiFaceInfo();
                multiFaceInfo.faceNum = 1;
                multiFaceInfo.faceOrients = new int[1];
                multiFaceInfo.faceOrients[0] = singleFaceInfo.faceOrient;
                multiFaceInfo.faceRects = new MRECT[1];
                multiFaceInfo.faceRects[0] = singleFaceInfo.faceRect;
                multiFaceInfo.faceDataInfoList = new FaceDataInfo[1];
                multiFaceInfo.faceDataInfoList[0] = singleFaceInfo.faceDataInfo;

                retCode = faceEngine.ASFProcessEx_IR(engineModel, image, multiFaceInfo, FaceEngineMask.ASF_IR_LIVENESS);
                if (retCode == 0 || retCode == 86019)
                {
                    retCode = faceEngine.ASFGetLivenessScore_IR(engineModel, out livenessInfo);
                }
            }
            catch (Exception ex)
            {
                LogUtil.LogInfo(typeof(FaceUtil), ex);
            }
            return livenessInfo;
        }

        /// <summary>
        /// Trích xuất khẩu trang
        /// </summary>
        /// <param name="pEngine"></param>
        /// <param name="imageInfo"></param>
        /// <param name="singleFaceInfo"></param>
        /// <returns></returns>
        private static MaskInfo maskEstimation(EngineModel engineModel, FaceEngine faceEngine, Image image, MultiFaceInfo multiFaceInfo)
        {
            MaskInfo maskInfo = new MaskInfo();
            int retCode = faceEngine.ASFProcessEx(engineModel, image, multiFaceInfo, FaceEngineMask.ASF_MASKDETECT);
            if (retCode == 0)
            {
                retCode = faceEngine.ASFGetMask(engineModel, out maskInfo);
            }

            if (retCode != 0)
            {
                throw new Exception(string.Format("Trích xuất trạng thái khẩu trang không thành công: {0}", retCode));
            }
            return maskInfo;
        }

        /// <summary>
        /// Trích xuất vùng trán
        /// </summary>
        /// <param name="pEngine"></param>
        /// <param name="imageInfo"></param>
        /// <param name="multiFaceInfo"></param>
        /// <returns></returns>
        public static LandMarkInfo landMarkEstimation(EngineModel engineModel, FaceEngine faceEngine, Image image, MultiFaceInfo multiFaceInfo)
        {
            LandMarkInfo landMarkInfo = new LandMarkInfo();
            int retCode = faceEngine.ASFProcessEx(engineModel, image, multiFaceInfo, FaceEngineMask.ASF_FACELANDMARK);
            if (retCode == 0)
            {
                retCode = faceEngine.ASFGetFaceLandMark(engineModel, out landMarkInfo);
            }

            if (retCode != 0)
            {
                throw new Exception(string.Format("Trích xuất vùng trán trên khuôn mặt không thành công: {0}", retCode));
            }
            return landMarkInfo;
        }

        /// <summary>
        /// Lấy khuôn mặt có kích thước lớn nhất
        /// </summary>
        /// <param name="multiFaceInfo"></param>
        /// <returns></returns>
        public static SingleFaceInfo GetMaxFace(MultiFaceInfo multiFaceInfo)
        {
            SingleFaceInfo singleFaceInfo = new SingleFaceInfo();
            singleFaceInfo.faceRect = new MRECT();
            singleFaceInfo.faceOrient = 1;

            int maxArea = 0;
            int index = -1;

            //Tìm khuôn mặt lớn nhất
            for (int i = 0; i < multiFaceInfo.faceNum; i++)
            {
                try
                {
                    MRECT rect = multiFaceInfo.faceRects[i];
                    int area = (rect.right - rect.left) * (rect.bottom - rect.top);
                    if (maxArea <= area)
                    {
                        maxArea = area;
                        index = i;
                    }
                }
                catch (Exception ex)
                {
                    LogUtil.LogInfo(typeof(FaceUtil), ex);
                }
            }

            //Lấy thông tin khuôn mặt tương ứng
            if (index != -1)
            {
                singleFaceInfo.faceRect = multiFaceInfo.faceRects[index];
                singleFaceInfo.faceOrient = multiFaceInfo.faceOrients[index];
                singleFaceInfo.faceDataInfo = multiFaceInfo.faceDataInfoList[index];
            }
            return singleFaceInfo;
        }

        /// <summary>
        /// Trích xuất chất lượng hình ảnh
        /// </summary>
        /// <param name="multiFaceInfo"></param>
        /// <returns></returns>
        private static float extractImageQuality(EngineModel workEngine, FaceEngine faceEngine, Image image, MultiFaceInfo multiFaceInfo, int faceIndex = 0, bool isMask = false)
        {
            float quality = 0;
            //int retCode = faceEngine.ASFProcessEx(image, multiFaceInfo, FaceEngineMask.ASF_IMAGEQUALITY);
            int retCode = 0;
            if (retCode == 0)
            {
                retCode = faceEngine.ASFImageQualityDetectEx(workEngine, image, multiFaceInfo, out quality, faceIndex, isMask);
            }

            if (retCode != 0)
            {
                throw new Exception(string.Format("Trích xuất chất lượng hình ảnh không thành công: {0}", retCode));
            }
            return quality;
        }

        /// <summary>
        /// Trích xuất feature khuôn mặt
        /// </summary>
        /// <param name="multiFaceInfo"></param>
        /// <returns></returns>
        private static FaceFeature extractFeature(EngineModel engineModel, FaceEngine faceEngine, Image image, MultiFaceInfo multiFaceInfo, int faceIndex = 0, bool isMask = false)
        {
            FaceFeature faceFeature = null;
            int retCode = faceEngine.ASFFaceFeatureExtractEx(engineModel, image, multiFaceInfo, ASF_RegisterOrNot.ASF_RECOGNITION, out faceFeature, faceIndex, isMask);
            if (retCode != 0)
            {
                throw new Exception(string.Format("Trích xuất đặc điểm không thành công, kết quả:{0}", retCode));
            }
            return faceFeature;
        }

        /// <summary>
        /// So sánh feature
        /// </summary>
        /// <param name="multiFaceInfo"></param>
        /// <returns></returns>
        public static float CompareFeature(FaceEngine faceEngine, FaceFeature faceFeature1, FaceFeature faceFeature2)
        {
            EngineModel engineModel = null;
            float similarity;

            try
            {
                engineModel = faceEngine.getEngineProcess();
                engineModel.EngineFree = false;

                faceEngine.ASFFaceFeatureCompare(engineModel, faceFeature1, faceFeature2, out similarity);

                if (similarity.ToString().IndexOf("E") > -1)
                {
                    similarity = 0f;
                }
            }catch(Exception ex)
            {
                throw ex;
            }
            finally
            {
                engineModel.EngineFree = true;
            }

            return similarity;
        }

        /// <summary>
        /// So sánh feature
        /// </summary>
        /// <param name="multiFaceInfo"></param>
        /// <returns></returns>
        public float CompareFeatureNew(EngineModel engineModel, FaceFeature faceFeature1, FaceFeature faceFeature2)
        {
            float similarity;

            try
            {
                faceEngine.ASFFaceFeatureCompare(engineModel, faceFeature1, faceFeature2, out similarity);

                if (similarity.ToString().IndexOf("E") > -1)
                {
                    similarity = 0f;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return similarity;
        }
    }
}
