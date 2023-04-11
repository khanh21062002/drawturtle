<template>
    <div ref="viewer" style="left: 0; right: 0; top: 0; bottom: 0" data-bind="dxReportViewer: $data"></div>
</template>

<script>
    import ko from "knockout";
    import 'devexpress-reporting/dx-webdocumentviewer';
    import { PreviewElements } from 'devexpress-reporting/dx-webdocumentviewer'
    var isDevEnv = process.env.NODE_ENV == "development";

    export default {
        name: "WebDocumentViewer",
        data() {
            return {
                previewModel: ko.observable(),
            }
        },
        props: {
            reportUrl: {
                type: String,
                required: true
            }
        },
        mounted() {
            var requestOptions = {
                // backend API
                host: isDevEnv ? 'http://localhost:1938/' : '/Service/',
                invokeAction: "DXXRDV"
            };
            var callbacks = {
                //Ẩn panel phải
                CustomizeElements: function (s, e) {
                    var panelPart = e.GetById(PreviewElements.RightPanel);
                    var index = e.Elements.indexOf(panelPart);
                    e.Elements.splice(index, 1);

                }
            };
            ko.applyBindings({
                reportUrl: ko.observable(this.reportUrl),
                viewerModel: this.previewModel,
                requestOptions: requestOptions,
                callbacks: callbacks
            }, this.$refs.viewer);
        },
        beforeDestroy() {
            ko.cleanNode(this.$refs.viewer);
        },
        methods: {
            refresh: function () {
                //this.previewModel().ApplyLocalization("en-");
                this.previewModel().StartBuild();
            },
            getClientParameter() {
                return this.previewModel().GetParametersModel();
            },
            openReport(url) {
                this.previewModel().OpenReport(url);
            },
        }
    };
</script>