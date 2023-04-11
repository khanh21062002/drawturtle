using ArcFaceSDK;
using ArcFaceSDK.Entity;
using ArcFaceSDK.SDKModels;
using ArcFaceSDK.Utils;
using ArcFaceService.Entity;
using ArcFaceService.Utils;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Threading;

namespace ArcFaceService.Service
{
    public class FaceServer
    {
        private FaceEngine faceEngine = null;
        private const int TOTAL_ENGINE_DEFAULT = 1;
        private static FaceServer _instance;
        private bool isUseSocketService = false;

        public static FaceServer Instance
        {
            get
            {
                return _instance == null ? _instance = new FaceServer() : _instance;
            }
        }

        public FaceServer()
        {
            isUseSocketService = Boolean.Parse(new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("AppSettings")["USE_ENGINE_SERVICE"]);
        }

        public void startEngine()
        {
            SocketClient.Instance.connectToServer();

            if (!isUseSocketService)
            {
                if (faceEngine == null)
                {
                    activeAndInitEngines();
                }

                FaceUtil.Instance.init(faceEngine);
            }
        }

        private void activeAndInitEngines()
        {
            faceEngine = new FaceEngine();

            try
            {
                int retCode = 0;
                bool isOnlineActive = Boolean.Parse(new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("AppSettings")["ACTIVE_ONLINE"]);
                try
                {
                    if (isOnlineActive)
                    {
                        #region Đọc tham số kích hoạt
                        string appId = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("AppSettings")["APPID"];
                        string sdkKey64 = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("AppSettings")["SDKKEY64"];
                        string activeKey64 = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("AppSettings")["ACTIVEKEY64"];

                        //Xác định loại CPU
                        var is64CPU = Environment.Is64BitProcess;
                        if (string.IsNullOrWhiteSpace(appId) || string.IsNullOrWhiteSpace(sdkKey64) || string.IsNullOrWhiteSpace(activeKey64))
                        {
                            Console.WriteLine(string.Format("Vui lòng thêm APP_ID và SDKKEY {0} trong tệp cấu hình App.config、ACTIVEKEY{0}!", is64CPU ? "64" : "32"));
                            return;
                        }
                        #endregion
                        retCode = faceEngine.ASFOnlineActivation(appId, sdkKey64, activeKey64);
                    }
                    else
                    {
                        #region Đọc path file active
                        string offlineActiveFilePath = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("AppSettings")["ACTIVE_FILE"];
                        string deviceInfo;
                        if (string.IsNullOrWhiteSpace(offlineActiveFilePath) || !File.Exists(offlineActiveFilePath))
                        {
                            retCode = faceEngine.ASFGetActiveDeviceInfo(out deviceInfo);
                            if (retCode != 0)
                            {
                                throw new Exception("Kích hoạt SDK không thành công, mã lỗi:" + retCode);
                            }
                            else
                            {
                                File.WriteAllText("ActiveDeviceInfo.txt", deviceInfo);
                            }
                            System.Environment.Exit(0);
                        }
                        #endregion
                        //Kích hoạt ngoại tuyến
                        retCode = faceEngine.ASFGetActiveDeviceInfo(out deviceInfo);
                        retCode = faceEngine.ASFOfflineActivation(offlineActiveFilePath);
                    }
                    if (retCode != 0 && retCode != 90114)
                    {
                        throw new Exception("Kích hoạt SDK không thành công, mã lỗi:" + retCode);
                        return;
                    }
                }
                catch (Exception ex)
                {
                    if (ex.Message.Contains("无法加载 DLL"))
                    {
                        throw new Exception("Vui lòng thêm DLL của SDK vào thư mục bin x86 hoặc x64 tương ứng!");
                    }
                    else
                    {
                        throw ex;
                    }
                    return;
                }

                //Init engine
                DetectionMode detectMode = DetectionMode.ASF_DETECT_MODE_IMAGE;

                //Góc phát hiện khuôn mặt
                ASF_OrientPriority imageDetectFaceOrientPriority = ASF_OrientPriority.ASF_OP_ALL_OUT;

                //Số lượng khuôn mặt tối đa được phát hiện cùng lúc
                int detectFaceMaxNum = 10;

                //Các tính năng engine sẽ sử dụng                
                int combinedMask = FaceEngineMask.ASF_FACE_DETECT | FaceEngineMask.ASF_FACERECOGNITION | FaceEngineMask.ASF_AGE | FaceEngineMask.ASF_GENDER | FaceEngineMask.ASF_FACE3DANGLE | FaceEngineMask.ASF_IMAGEQUALITY | FaceEngineMask.ASF_MASKDETECT | FaceEngineMask.ASF_LIVENESS;

                //Lấy tổng số engine sẽ được khởi tạo
                int totalEngine = TOTAL_ENGINE_DEFAULT;
                try
                {
                    totalEngine = Int32.Parse(new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("AppSettings")["TOTAL_ENGINE"]);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
                faceEngine.setTotalEngine(totalEngine);

                //Khởi tạo
                retCode = faceEngine.ASFInitEngine(detectMode, imageDetectFaceOrientPriority, detectFaceMaxNum, combinedMask);
                Console.WriteLine("InitEngine Result:" + ((retCode == 0) ? "Face engine khởi tạo thành công!" : string.Format("Face engine khởi tạo không thành công! Mã lỗi: {0}", retCode)));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void addFeature(PersonUnit personUnit)
        {
            var requestId = Guid.NewGuid().ToString();
            var httpRequest = new RequestMessage<string, bool>(requestId, HttpType.ADD_FEATURE, personUnit.personId, false);
            var requestJson = JsonConvert.SerializeObject(httpRequest);
            string responseJson = SocketClient.Instance.sendRequest(requestJson + "\r\n").Result;
            try
            {
                var httpResponse = JsonConvert.DeserializeObject<RequestMessage<string, bool>>(responseJson);
                bool result = httpResponse.output;
                Console.WriteLine("Add feature by socket " + result);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error add feature by socket " + ex.Message);
            }
        }

        public void updateFeature(PersonUnit personUnit)
        {
            var requestId = Guid.NewGuid().ToString();
            var httpRequest = new RequestMessage<string, bool>(requestId, HttpType.UPDATE_FEATURE, personUnit.personId, false);
            var requestJson = JsonConvert.SerializeObject(httpRequest);
            string responseJson = SocketClient.Instance.sendRequest(requestJson + "\r\n").Result;
            try
            {
                var httpResponse = JsonConvert.DeserializeObject<RequestMessage<string, bool>>(responseJson);
                bool result = httpResponse.output;
                Console.WriteLine("Update feature by socket " + result);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error update feature by socket " + ex.Message);
            }
        }

        public void updateListFeature(List<PersonUnit> lsPerson)
        {
            Console.WriteLine("Log updateCacheFeature start " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss zzz"));
            for (int i = 0; i < lsPerson.Count; i++)
            {
                try
                {
                    Console.WriteLine("Log updateCacheFeature " + i);
                    PersonUnit personUnit = lsPerson[i];
                    updateFeature(personUnit);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error cache person to ram " + ex.Message);
                }
            }
            Console.WriteLine("Log updateCacheFeature end " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss zzz"));
        }

        public void removeFeature(PersonUnit personUnit)
        {
            var requestId = Guid.NewGuid().ToString();
            var httpRequest = new RequestMessage<string, bool>(requestId, HttpType.REMOVE_FEATURE, personUnit.personId, false);
            var requestJson = JsonConvert.SerializeObject(httpRequest);
            string responseJson = SocketClient.Instance.sendRequest(requestJson + "\r\n").Result;
            try
            {
                var httpResponse = JsonConvert.DeserializeObject<RequestMessage<string, bool>>(responseJson);
                bool result = httpResponse.output;
                Console.WriteLine("Remove feature by socket " + result);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error remove feature by socket " + ex.Message);
            }
        }

        public void removeGroupFeature(int groupId)
        {
            var requestId = Guid.NewGuid().ToString();
            var httpRequest = new RequestMessage<string, bool>(requestId, HttpType.REMOVE_GROUP, "" + groupId, false);
            var requestJson = JsonConvert.SerializeObject(httpRequest);
            string responseJson = SocketClient.Instance.sendRequest(requestJson + "\r\n").Result;
            try
            {
                var httpResponse = JsonConvert.DeserializeObject<RequestMessage<string, bool>>(responseJson);
                bool result = httpResponse.output;
                Console.WriteLine("Remove group feature by socket " + result);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error remove group feature by socket " + ex.Message);
            }
        }        

        /// <summary>
        /// Trích xuất thuộc tính khuôn mặt
        /// </summary>
        /// <param name="faceInput">Ảnh đầu vào</param>
        /// <param name="faceType">Loại ảnh đầu vào</param>
        /// <returns>Tất cả thuộc tính của khuôn mặt</returns>
        public FaceResult<string, FaceFeatureUnit> analyzeFace(string faceInput, int faceType, bool checkBusy = false)
        {
            if (isUseSocketService)
            {
                return analyzeFaceByService(faceInput, faceType, checkBusy);
            }
            else
            {
                return analyzeFaceByWeb(faceInput, faceType);
            }
        }

        /// <summary>
        /// Trích xuất thuộc tính khuôn mặt bằng service
        /// </summary>
        /// <param name="faceInput">Ảnh đầu vào</param>
        /// <param name="faceType">Loại ảnh đầu vào</param>
        /// <returns>Tất cả thuộc tính của khuôn mặt</returns>
        private FaceResult<string, FaceFeatureUnit> analyzeFaceByService(string faceInput, int faceType, bool checkBusy = false)
        {
            var faceResult = new FaceResult<string, FaceFeatureUnit>();
            faceResult.status = 200;

            try
            {
                var requestId = Guid.NewGuid().ToString();
                var imageInput = new ImageInput(faceInput, faceType);
                var httpRequest = new RequestMessage<ImageInput, FaceFeatureUnit>(requestId, HttpType.DETECT, imageInput, null);

                var requestJson = JsonConvert.SerializeObject(httpRequest);
                string responseJson = SocketClient.Instance.sendRequest((checkBusy ? "NOWAIT" : "WAIT") + requestJson + "\r\n").Result;
                try
                {
                    var httpResponse = JsonConvert.DeserializeObject<RequestMessage<ImageInput, FaceFeatureUnit>>(responseJson);
                    FaceFeatureUnit featureUnit = httpResponse.output;

                    if (featureUnit == null)
                    {
                        faceResult.status = 406;
                        faceResult.error = ErrorMessage.ERR_NO_EXISTS_FACE;
                        return faceResult;
                    }

                    if (featureUnit.feature.Length == 0 && featureUnit.featureBase64.Length > 0)
                    {
                        var feature = System.Convert.FromBase64String(featureUnit.featureBase64);
                        featureUnit.feature = feature;
                    }

                    faceResult.input_data = faceInput;
                    faceResult.result = featureUnit;
                }
                catch (Exception ex)
                {
                    faceResult.status = 500;
                    faceResult.error = responseJson;
                }
            }
            catch (Exception ex)
            {
                faceResult.status = 500;
                faceResult.error = ex.Message;
            }

            return faceResult;
        }

        /// <summary>
        /// Trích xuất thuộc tính khuôn mặt trực tiếp
        /// </summary>
        /// <param name="faceInput">Ảnh đầu vào</param>
        /// <param name="faceType">Loại ảnh đầu vào</param>
        /// <returns>Tất cả thuộc tính của khuôn mặt</returns>
        private FaceResult<String, FaceFeatureUnit> analyzeFaceByWeb(string faceInput, int faceType)
        {
            var faceResult = new FaceResult<string, FaceFeatureUnit>();
            faceResult.status = 200;

            try
            {
                Image image = BaseUtils.getFaceImage(faceInput, faceType);
                BaseUtils.checkImageWidthAndHeight(ref image);

                List<FaceFeatureUnit> lsFaceFeature = FaceUtil.Instance.FaceFeatures(image, faceEngine, true);
                if (lsFaceFeature.Count == 0)
                {
                    faceResult.status = 406;
                    faceResult.error = ErrorMessage.ERR_NO_EXISTS_FACE;
                    return faceResult;
                }

                if (lsFaceFeature.Count > 1)
                {
                    faceResult.status = 500;
                    faceResult.error = ErrorMessage.ERR_MORE_THAN_ONE_FACE;
                    return faceResult;
                }

                faceResult.input_data = faceInput;
                faceResult.result = lsFaceFeature[0];
            }
            catch (Exception ex)
            {
                faceResult.status = 500;
                faceResult.error = ex.Message;
            }

            return faceResult;
        }

        /// <summary>
        /// So sánh điểm matching giữa 2 khuôn mặt
        /// </summary>
        /// <param name="faceInput1">Ảnh vào 1</param>
        /// <param name="faceInput2">Ảnh vào 2</param>
        /// <param name="faceType1">Loại ảnh đầu vào: 1-URL, 2-FILE, 3-BASE64</param>
        /// <param name="faceType2">Loại ảnh đầu vào: 1-URL, 2-FILE, 3-BASE64</param>
        /// <returns>Điểm số matching khoảng 0-1</returns>
        public FaceResult<object, float> compareFace(string faceInput1, string faceInput2, int faceType1, int faceType2)
        {
            if (isUseSocketService)
            {
                return compareFaceByService(faceInput1, faceInput2, faceType1, faceType2);
            }
            else
            {
                return compareFaceByWeb(faceInput1, faceInput2, faceType1, faceType2);
            }
        }

        /// <summary>
        /// So sánh điểm matching giữa 2 khuôn mặt bằng service
        /// </summary>
        /// <param name="faceInput1">Ảnh vào 1</param>
        /// <param name="faceInput2">Ảnh vào 2</param>
        /// <param name="faceType1">Loại ảnh đầu vào: 1-URL, 2-FILE, 3-BASE64</param>
        /// <param name="faceType2">Loại ảnh đầu vào: 1-URL, 2-FILE, 3-BASE64</param>
        /// <returns>Điểm số matching khoảng 0-1</returns>
        public FaceResult<object, float> compareFaceByService(string faceInput1, string faceInput2, int faceType1, int faceType2)
        {
            var faceResult = new FaceResult<object, float>();

            try
            {
                var requestId = Guid.NewGuid().ToString();
                var imageInput1 = new ImageInput(faceInput1, faceType1);
                var imageInput2 = new ImageInput(faceInput2, faceType2);

                var compareInput = new CompareInput(imageInput1, imageInput2);
                var httpRequest = new RequestMessage<CompareInput, float>(requestId, HttpType.COMPARE, compareInput, 0f);

                var requestJson = JsonConvert.SerializeObject(httpRequest);
                string responseJson = SocketClient.Instance.sendRequest(requestJson + "\r\n").Result;

                try
                {
                    var httpResponse = JsonConvert.DeserializeObject<RequestMessage<CompareInput, float>>(responseJson);
                    float matchingScore = httpResponse.output;

                    faceResult.status = 200;
                    faceResult.result = matchingScore;
                }
                catch
                {
                    faceResult.status = 500;
                    faceResult.error = responseJson;
                }
            }
            catch (Exception ex)
            {
                faceResult.status = 500;
                faceResult.error = ex.Message;
                faceResult.result = 0;
            }

            return faceResult;
        }

        /// <summary>
        /// So sánh điểm matching giữa 2 khuôn mặt bằng web
        /// </summary>
        /// <param name="faceInput1">Ảnh vào 1</param>
        /// <param name="faceInput2">Ảnh vào 2</param>
        /// <param name="faceType1">Loại ảnh đầu vào: 1-URL, 2-FILE, 3-BASE64</param>
        /// <param name="faceType2">Loại ảnh đầu vào: 1-URL, 2-FILE, 3-BASE64</param>
        /// <returns>Điểm số matching khoảng 0-1</returns>
        public FaceResult<Object, float> compareFaceByWeb(string faceInput1, string faceInput2, int faceType1, int faceType2)
        {
            var faceResult = new FaceResult<Object, float>();
            EngineModel engineProcess = null;

            try
            {
                Image image1 = BaseUtils.getFaceImage(faceInput1, faceType1);
                Image image2 = BaseUtils.getFaceImage(faceInput2, faceType2);

                BaseUtils.checkImageWidthAndHeight(ref image1);
                BaseUtils.checkImageWidthAndHeight(ref image2);

                engineProcess = faceEngine.getEngineProcess();

                List<FaceFeatureUnit> lsFaceFeature1 = FaceUtil.Instance.FaceFeaturesNew(image1, engineProcess, false);
                List<FaceFeatureUnit> lsFaceFeature2 = FaceUtil.Instance.FaceFeaturesNew(image2, engineProcess, false);

                //Validate khuôn mặt trong ảnh đầu vào
                if (lsFaceFeature1.Count == 0)
                {
                    faceResult.status = 406;
                    faceResult.error = ErrorMessage.ERR_NO_EXISTS_FACE;
                    return faceResult;
                }

                if (lsFaceFeature1.Count > 1)
                {
                    faceResult.status = 400;
                    faceResult.error = ErrorMessage.ERR_MORE_THAN_ONE_FACE;
                    return faceResult;
                }

                if (lsFaceFeature1[0].feature == null)
                {
                    faceResult.status = 406;
                    faceResult.error = ErrorMessage.ERR_NO_EXISTS_FACE;
                    return faceResult;
                }

                if (lsFaceFeature2.Count == 0)
                {
                    faceResult.status = 406;
                    faceResult.error = ErrorMessage.ERR_NO_EXISTS_FACE;
                    return faceResult;
                }

                if (lsFaceFeature2.Count != 1)
                {
                    faceResult.status = 400;
                    faceResult.error = ErrorMessage.ERR_MORE_THAN_ONE_FACE;
                    return faceResult;
                }

                if (lsFaceFeature2[0].feature == null)
                {
                    faceResult.status = 406;
                    faceResult.error = ErrorMessage.ERR_NO_EXISTS_FACE;
                    return faceResult;
                }
                //End validate

                FaceFeature faceFeature1 = new FaceFeature();
                faceFeature1.feature = lsFaceFeature1[0].feature;
                faceFeature1.featureSize = lsFaceFeature1[0].feature.Length;

                FaceFeature faceFeature2 = new FaceFeature();
                faceFeature2.feature = lsFaceFeature2[0].feature;
                faceFeature2.featureSize = lsFaceFeature2[0].feature.Length;

                float matchingScore = FaceUtil.Instance.CompareFeatureNew(engineProcess, faceFeature1, faceFeature2);
                faceResult.status = 200;
                faceResult.result = matchingScore;
            }
            catch (Exception ex)
            {
                faceResult.status = 500;
                faceResult.error = ex.Message;
                faceResult.result = 0;
            }
            finally
            {
                if (engineProcess != null)
                {
                    faceEngine.releaseEngine(engineProcess);
                }
            }
            return faceResult;
        }

        /// <summary>
        /// Tìm kiếm khuôn mặt trong CSDL trùng với khuôn mặt được nhập bằng service
        /// </summary>
        /// <param name="faceInput">Ảnh vào</param>
        /// <param name="faceType">Loại ảnh đầu vào</param>
        /// <param name="groupId">ID nhóm collection</param>
        /// <param name="threshold">Ngưỡng chấp nhận matching [0-1]</param>
        /// <returns>Danh sách khuôn mặt có điểm số lớn hơn threshold</returns>
        public FaceResult<FaceFeatureUnit, List<PersonResult>> searchFace(string faceInput, int faceType, List<int> lsGroupId, float threshold, bool checkBusy = false)
        {
            var faceResult = new FaceResult<FaceFeatureUnit, List<PersonResult>>();

            try
            {
                var requestId = Guid.NewGuid().ToString();
                var groupIds = lsGroupId.ToArray();
                var imageInput = new ImageInput(faceInput, faceType, groupIds, threshold);

                var httpRequest = new RequestMessage<ImageInput, List<PersonResult>>(requestId, HttpType.SEARCH_FACE, imageInput, null);

                var requestJson = JsonConvert.SerializeObject(httpRequest);
                string responseJson = SocketClient.Instance.sendRequest((checkBusy ? "NOWAIT" : "WAIT") + requestJson + "\r\n").Result;


                try
                {
                    var httpResponse = JsonConvert.DeserializeObject<RequestMessage<ImageInput, FaceResult<FaceFeatureUnit, List<PersonResult>>>>(responseJson);
                    FaceResult<FaceFeatureUnit, List<PersonResult>> searchResult = httpResponse.output;

                    faceResult.status = 200;
                    faceResult.error = ErrorMessage.ERR_SUCCESS;
                    faceResult.input_data = searchResult.input_data;
                    faceResult.result = searchResult.result;
                }
                catch
                {
                    //Trường hợp lỗi return thằng từ engine server
                    faceResult.status = 500;
                    faceResult.error = responseJson;
                }
            }
            catch (Exception ex)
            {
                faceResult.status = 500;
                faceResult.error = ex.Message;
            }

            return faceResult;
        }

        /// <summary>
        /// Xác thực khuôn mặt đầu vào của một người so với danh sách khuôn mặt của chính họ
        /// </summary>
        /// <param name="faceInput">Ảnh vào</param>
        /// <param name="faceType">Loại ảnh đầu vào</param>
        /// <param name="threshold">Ngưỡng chấp nhận matching [0-1]</param>
        /// <param name="lsSource">Danh sách feature của người đó trong CSDL</param>
        /// <returns>Danh sách khuôn mặt có điểm số lớn hơn threshold</returns>
        public FaceResult<FaceFeatureUnit, List<PersonResult>> authenticatePerson(string faceInput, int faceType, List<PersonUnit> lsSource)
        {
            if (isUseSocketService)
            {
                return authenticatePersonByService(faceInput, faceType, lsSource);
            }
            else
            {
                return authenticatePersonByWeb(faceInput, faceType, lsSource);
            }
        }

        /// <summary>
        /// Xác thực khuôn mặt đầu vào của một người so với danh sách khuôn mặt của chính họ bằng service
        /// </summary>
        /// <param name="faceInput">Ảnh vào</param>
        /// <param name="faceType">Loại ảnh đầu vào</param>
        /// <param name="threshold">Ngưỡng chấp nhận matching [0-1]</param>
        /// <param name="lsSource">Danh sách feature của người đó trong CSDL</param>
        /// <returns>Danh sách khuôn mặt có điểm số lớn hơn threshold</returns>
        public FaceResult<FaceFeatureUnit, List<PersonResult>> authenticatePersonByService(string faceInput, int faceType, List<PersonUnit> lsSource)
        {
            var faceResult = new FaceResult<FaceFeatureUnit, List<PersonResult>>();
            var imageSearch = new ImageInput(faceInput, faceType);

            try
            {
                List<PersonResult> lsPersonMatched = new List<PersonResult>();
                foreach (PersonUnit source in lsSource)
                {
                    if (source.faceFeature == null)
                        continue;

                    var requestId = Guid.NewGuid().ToString();

                    var featureBase64 = Convert.ToBase64String(source.faceFeature);
                    var imageInCollection = new ImageInput(featureBase64, FaceType.FEATURE);

                    var compareInput = new CompareInput(imageSearch, imageInCollection);

                    var httpRequest = new RequestMessage<CompareInput, float>(requestId, HttpType.COMPARE, compareInput, 0f);
                    var requestJson = JsonConvert.SerializeObject(httpRequest);
                    string responseJson = SocketClient.Instance.sendRequest(requestJson + "\r\n").Result;

                    try
                    {
                        var httpResponse = JsonConvert.DeserializeObject<RequestMessage<CompareInput, float>>(responseJson);
                        float score = httpResponse.output;

                        PersonResult personMatched = new PersonResult(source.personId, source.personCode, source.personName, source.facePath, score, source.passport, source.phonenumber, source.birthday);
                        lsPersonMatched.Add(personMatched);
                    }
                    catch
                    {
                        //Trường hợp lỗi return thằng từ engine server
                        faceResult.status = 500;
                        faceResult.error = responseJson;
                        return faceResult;
                    }
                }

                faceResult.status = 1;
                faceResult.error = ErrorMessage.ERR_SUCCESS;
                faceResult.input_data = null;// lsFaceFeature[0];
                faceResult.result = lsPersonMatched;
            }
            catch (Exception ex)
            {
                faceResult.status = 0;
                faceResult.error = ex.Message;
            }

            return faceResult;
        }

        /// <summary>
        /// Xác thực khuôn mặt đầu vào của một người so với danh sách khuôn mặt của chính họ bằng web
        /// </summary>
        /// <param name="faceInput">Ảnh vào</param>
        /// <param name="faceType">Loại ảnh đầu vào</param>
        /// <param name="threshold">Ngưỡng chấp nhận matching [0-1]</param>
        /// <param name="lsSource">Danh sách feature của người đó trong CSDL</param>
        /// <returns>Danh sách khuôn mặt có điểm số lớn hơn threshold</returns>
        private FaceResult<FaceFeatureUnit, List<PersonResult>> authenticatePersonByWeb(string faceInput, int faceType, List<PersonUnit> lsSource)
        {
            var faceResult = new FaceResult<FaceFeatureUnit, List<PersonResult>>();

            try
            {
                Image image = BaseUtils.getFaceImage(faceInput, faceType);
                BaseUtils.checkImageWidthAndHeight(ref image);

                List<FaceFeatureUnit> lsFaceFeature = FaceUtil.Instance.FaceFeatures(image, faceEngine, false);

                if (lsFaceFeature.Count == 0)
                {
                    faceResult.status = 0;
                    faceResult.error = ErrorMessage.ERR_NO_EXISTS_FACE;
                    return faceResult;
                }

                if (lsFaceFeature.Count > 1)
                {
                    faceResult.status = 0;
                    faceResult.error = ErrorMessage.ERR_MORE_THAN_ONE_FACE;
                    return faceResult;
                }

                List<PersonResult> lsPersonMatched = new List<PersonResult>();
                foreach (PersonUnit source in lsSource)
                {
                    if (source.faceFeature == null) continue;

                    FaceFeature featureInput = new FaceFeature();
                    featureInput.feature = lsFaceFeature[0].feature;
                    featureInput.featureSize = lsFaceFeature[0].feature.Length;

                    FaceFeature featureInCollection = new FaceFeature();
                    featureInCollection.feature = source.faceFeature;
                    featureInCollection.featureSize = source.faceFeature.Length;

                    float score = FaceUtil.CompareFeature(faceEngine, featureInput, featureInCollection);
                    PersonResult personMatched = new PersonResult(source.personId, source.personCode, source.personName, source.facePath, score, source.passport, source.phonenumber, source.birthday);
                    lsPersonMatched.Add(personMatched);
                }

                faceResult.status = 1;
                faceResult.error = ErrorMessage.ERR_SUCCESS;
                faceResult.input_data = lsFaceFeature[0];
                faceResult.result = lsPersonMatched;
            }
            catch (Exception ex)
            {
                faceResult.status = 0;
                faceResult.error = ex.Message;
            }

            return faceResult;
        }
    }
}
