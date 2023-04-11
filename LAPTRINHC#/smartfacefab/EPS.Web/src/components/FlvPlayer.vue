<template>
    <div>
        <slot></slot>
    </div>
</template>

<script>
    //import { NodePlayer } from '../assets/jquery/NodePlayer.min.js';
    export default {
        name: 'flvPlayer',
        props: {
            width: {
                type: Number,
                default: 800,
            },
            height: {
                type: Number,
                default: 600,
            },
            videoIndex: {
                type: Number,
                default: 0,
            },
            source: {
                type: String,
                default: '',
            },
        },
        mixins: [],
        components: {},
        data() {
            return {
                player: null,
            };
        },
        computed: {},
        watch: {
            source(val) {
                if (val) {
                    this.changeSource();
                }
            },
        },
        methods: {
            init() {
                var canv = document.getElementById('video' + this.videoIndex);
                this.player = new NodePlayer();
                this.player.setView(canv.id);
                this.player.setScaleMode(0);
                this.player.setBufferTime(100);
                this.player.skipLoopFilter(32);

                this.player.on('start', () => {
                    console.log("player on start");
                });

                this.player.on('error', (e) => {
                    console.log("player on error", e);
                });

                this.player.on('stop', () => {
                    console.log("player on stop");
                });

                if (this.source)
                    this.player.start(this.source);
                //event fullscreen
                $('#fullscreen' + this.videoIndex).click(function () {
                    if (canv.webkitRequestFullScreen) {
                        canv.webkitRequestFullScreen();
                    } else {
                        canv.mozRequestFullScreen();
                    }
                });
            },
            changeSource() {
                this.player.stop();
                this.player.start(this.source);
            },
        },
        created() { },
        mounted() {
            this.init();
        },
        beforeCreate() { },
        beforeMount() { },
        beforeUpdate() { },
        updated() { },
        beforeDestroy() {
            //Stop player when redirect
            this.player.stop();
        },
        destroyed() {
        },
        activated() { },
    };
</script>

<style lang="scss">
    .flv-player-style {
        width: 100%;
        height: 100%;
        background-color: black;
    }

    .flv-fullscreen {
        position: absolute;
        bottom: 5px;
        right: 5px;
    }

    .flv-reload-btn {
        position: absolute;
        bottom: 5px;
        right: 55px;
    }

    .flv-add-btn {
        position: absolute;
        bottom: 5px;
        right: 105px;
    }
</style>
