<template>
    <router-view></router-view>

</template>

<script>
    export default {
        name: 'App',
        updated() {
            //debugger;
            //let modernizrScript = document.createElement('script')
            //modernizrScript.setAttribute('src', '/assets/vendor/modernizr/modernizr.js')
            //document.head.appendChild(modernizrScript)


            //let jqueryScript = document.createElement('script')
            //jqueryScript.setAttribute('src', '/assets/vendor/jquery/jquery.js')
            //document.body.appendChild(jqueryScript)

            let themeScript = document.createElement('script')
            themeScript.setAttribute('src', '/assets/js/theme.js')
            document.body.appendChild(themeScript)

            //let themeInitScript = document.createElement('script')
            //themeInitScript.setAttribute('src', '/assets/js/theme.init.js')
            //document.body.appendChild(themeInitScript)
        },
    }
</script>

<style lang="scss">
/*    @font-face {
        font-family: "poppins";
        src: url(assets/font/Poppins-Regular.ttf) format("truetype");
    }

    @font-face {
        font-family: "poppinsExtraBold";
        src: url(assets/font/Poppins-ExtraBold.ttf) format("truetype");
    }*/

    #app {
        font-family: 'poppins', Helvetica, Arial, sans-serif;
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
