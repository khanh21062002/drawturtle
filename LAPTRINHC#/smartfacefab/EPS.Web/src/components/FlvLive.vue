<template>
    <div class="liveView" style="height: 100%; border: 1px solid #333">
        <FlvPlayer :videoIndex="videoIndex" class="flv-player-style" :source="sources">
            <canvas :id="`video${videoIndex}`" width="1500" height="600" class="flv-player-style"></canvas>
            <button :id="`fullscreen${videoIndex}`"
                    :title="$t('Button.Fullscreen')"
                    type="button"
                    class="btn btn-secondary flv-fullscreen">
                <i data-v-59b19dfe="" class="fa fa-arrows-alt"></i>
            </button>
            <button type="button"
                    class="btn btn-secondary flv-reload-btn"
                    :title="$t('Button.Refresh')"
                    @click="submitSelected()">
                <i data-v-59b19dfe="" class="fa fa-refresh"></i>
            </button>
            <button type="button" 
                    class="btn btn-secondary flv-add-btn"
                    :title="$t('Button.AddCam')"
                    @click="popupCamera">
                <i data-v-59b19dfe="" class="fa fa-plus"></i>
            </button>
        </FlvPlayer>
    </div>
</template>

<script>
    import md5 from 'js-md5';

    const isDevEnv = process.env.NODE_ENV === 'development';

    const generatorExpiration = function (privateKey, compId, endpoint = null) {
        let md5Hash = null;
        const timeStamp = Math.floor(Date.now() / 1000) + 100;
        if (endpoint) {
            md5Hash = md5(`/BOTO_${compId}/${endpoint}-${timeStamp}-${privateKey}`);
        } else {
            md5Hash = md5(`${timeStamp}-${privateKey}`);
        }
        return {
            md5Hash,
            timeStamp,
            compId,
        };
    };

    export default {
        name: 'live',
        props: {
            data: {
                type: [String, Number, Object, Array],
                require: true,
            },
            videoIndex: {
                type: Number,
                require: true,
            },
            systemCode: {
                type: String, required: true
            },
            handleShowModal: {
                type: Function, required: true
            }
        },
        data() {
            return {
                sources: '',
                streamSource: '',
                cameraCode: '',
                areaCode:''
            };
        },
        computed: {},
        created() {
            //this.submitSelected();
        },
        beforeDestroy() { },
        methods: {
            submitSelected(data = null) {
                if (data != null) {
                    this.streamSource = data.streamLink;
                    this.areaCode = data.areaCode;
                    this.cameraCode = data.cameraCode;
                    const accessToken = this.$services.getAccessToken();
                    const privateKey = accessToken.nodeMediaPrivateKey || 'nodemedia2017privatekey';
                    const compId = accessToken.comId;
                    const endPoint = `${data.areaCode}$$${data.cameraCode}`;
                    const { timeStamp, md5Hash } = generatorExpiration(privateKey, compId, endPoint)
                    this.sources = `${data.streamLink}${this.systemCode}_${compId}/${endPoint}.flv?sign=${timeStamp}-${md5Hash}`;
                }
                else {
                    const accessToken = this.$services.getAccessToken();
                    const privateKey = accessToken.nodeMediaPrivateKey || 'nodemedia2017privatekey';
                    const compId = accessToken.comId;
                    const endPoint = `${this.areaCode}$$${this.cameraCode}`;
                    const { timeStamp, md5Hash } = generatorExpiration(privateKey, compId, endPoint)
                    this.sources = `${this.streamSource}${this.systemCode}_${compId}/${endPoint}.flv?sign=${timeStamp}-${md5Hash}`;
                }
            },
            popupCamera() {
                this.handleShowModal(true, this.videoIndex);
            },
        },
    };
</script>

<style scoped>
    .liveView {
        position: relative;
    }

    .selectWrapper {
        display: flex;
        justify-content: center;
        align-items: center;
        font-size: 20px;
        line-height: 30px;
        margin: 20px;
        vertical-align: baseline;
    }

    .inputWrapper {
        width: 500px;
        margin: 0 auto;
    }

    .buttonWrapper {
        text-align: center;
    }

    .bottom-right {
        position: absolute;
        top: 8%;
        right: 8%;
        color: orange;
    }
</style>
