<template>
    <router-view></router-view>
</template>

<script>
    import Services from '@/utils/services'

    function GetChildByUid(childArr, childUid) {
        var ret = null;
        var childArrLen = Object.keys(childArr).length;
        for (var i = 0; i < childArrLen; i++) {
            var num = Number(childArr[i]._uid);
            if (num === childUid) {
                ret = childArr[i];
                break;
            } else if (Object.keys(childArr[i]).length > 0) {
                ret = GetChildByUid(childArr[i].$children, childUid);
            }
            if (ret != null) {
                break;
            }
        }
        return ret;
    }
    export default {
        name: 'App',
        data() {
            return {
                idComp: null,
            }
        },
        sockets: {
            eventRealTime(event) {
                var child = GetChildByUid(this.$children, Number(2000000002));
                if (child != null) {
                    if (this.idComp == event.compId) {
                        child.listEvent = child.listEvent.filter(x => x.personType == 1);
                        if (child.listEvent.length >= 10) {
                            child.listEvent.pop();
                            child.listEvent.unshift(event);
                        }
                        else {
                            child.listEvent.unshift(event);
                        }
                    }
                }
            },
            eventGuessRealTime(event) {
                var child = GetChildByUid(this.$children, Number(2000000002));
                if (child != null) {
                    if (this.idComp == event.compId) {
                        child.listEventGuess = child.listEventGuess.filter(x => x.personType == 2);
                        if (child.listEventGuess.length >= 10) {
                            child.listEventGuess.pop();
                            child.listEventGuess.unshift(event);
                        }
                        else {
                            child.listEventGuess.unshift(event);
                        }
                    }
                }
            },
            eventBlackListRealTime(event) {
                var child = GetChildByUid(this.$children, Number(2000000002));
                if (child != null) {
                    if (this.idComp == event.compId) {
                        child.listEventBlackList = child.listEventBlackList.filter(x => x.personType == 4 || x.personType == 5 || x.personType == 6 || x.personType == 7);
                        if (child.listEventBlackList.length >= 10) {
                            child.listEventBlackList.pop();
                            child.listEventBlackList.unshift(event);
                        }
                        else {
                            child.listEventBlackList.unshift(event);
                        }
                    }
                }
            },
        },
        created() {
            var accessToken = Services.getAccessToken();
            this.idComp = accessToken.comId;
        },
    };
</script>

<style lang="scss">
    /* Import Main styles for this application */
    @import 'assets/scss/style';

    .select2 {
        width: 100% !important;
    }

    .select2-selection--single {
        height: 100% !important;
    }

    .select2-selection--multiple {
        height: 100% !important;
    }

    .select2-selection__rendered {
        word-wrap: break-word !important;
        text-overflow: inherit !important;
        white-space: normal !important;
    }

    .select2-container {
        height: auto !important;
    }

    .select2-selection__rendered {
        height: auto !important;
    }

    #app {
        font-family: 'BaiJamjureeRegular', Helvetica, Arial, sans-serif;
        -webkit-font-smoothing: antialiased;
        -moz-osx-font-smoothing: grayscale;
        text-align: left;
        color: #4f5d73c9;
        margin-top: 60px;
    }

    @font-face {
        font-family: "NasalizationRg";
        src: url(assets/font/nasalization-rg.ttf) format("truetype");
    }

    @font-face {
        font-family: "BaiJamjureeBold";
        src: url(assets/font/BaiJamjuree-Bold.ttf) format("truetype");
    }

    @font-face {
        font-family: "BaiJamjureeItalic";
        src: url(assets/font/BaiJamjuree-Italic.ttf) format("truetype");
    }

    @font-face {
        font-family: "BaiJamjureeLight";
        src: url(assets/font/BaiJamjuree-Light.ttf) format("truetype");
    }

    @font-face {
        font-family: "BaiJamjureeRegular";
        src: url(assets/font/BaiJamjuree-Regular.ttf) format("truetype");
    }

    @font-face {
        font-family: "BaiJamjureeSemiBold";
        src: url(assets/font/BaiJamjuree-SemiBold.ttf) format("truetype");
    }

    @font-face {
        font-family: "helveticaneue2";
        src: url(assets/font/helveticaneue_2.ttf) format("truetype");
    }
</style>
