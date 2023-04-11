<template>
    <div class="upload-container row">
        <div v-for="(item, index) in uploadedFiles" :key="index" class="img-wrap col-md-3">
            <span class="remove-img" @click="uploadedFiles.splice(index, 1)">&times;</span>
            <template v-if="item.isSuccess">
                <img :src="getUrl(item.filePath)" class="img-responsive img-thumbnail" :alt="item.originalName">
            </template>
            <template v-else>
                <img src="error.png" class="img-responsive img-thumbnail" :alt="`Error uploading ${item.originalName}. Message:  ${item.Message}`">
            </template>
        </div>
        <!--UPLOAD-->
        <form enctype="multipart/form-data" novalidate v-if="isInitial || isSaving" class="col-md-3">
            <h3>Upload images</h3>
            <div class="dropbox">
                <input type="file" :multiple="multiple" :disabled="disabled" name="files" @change="filesChange($event.target.name, $event.target.files); fileCount = $event.target.files.length"
                       accept="image/*" class="input-file">
                <p v-if="isInitial">
                    Drag your file(s) here to begin<br> or click to browse
                </p>
                <p v-if="isSaving">
                    Uploading {{ fileCount }} files...
                </p>
            </div>
        </form>
        <!--SUCCESS-->
        <div v-if="isSuccess" class="col-md-3">
            <h3>Uploaded {{ fileCount }} file(s) successfully.</h3>
            <p>
                <a href="javascript:void(0)" @click="reset()">Upload again</a>
            </p>
        </div>
        <!--FAILED-->
        <div v-if="isFailed" class="col-md-3">
            <h3>Uploaded failed.</h3>
            <p>
                <a href="javascript:void(0)" @click="reset()">Try again</a>
            </p>
            <pre>{{ uploadError }}</pre>
        </div>
    </div>
</template>
<script>
    const STATUS_INITIAL = 0, STATUS_SAVING = 1, STATUS_SUCCESS = 2, STATUS_FAILED = 3;

    export default {
        props: {
            multiple_files: {
                type: Boolean,
                default: false
            },
            files: String
        },
        model: {
            prop: 'files',
            event: 'fileUploaded'
        },
        data() {
            return {
                uploadedFiles: ((this.files || '').split(',') || []).map(path => { return { filePath: path, isSuccess: true }; }),
                uploadError: null,
                currentStatus: STATUS_INITIAL,
                multiple: this.multiple_files,
                fileCount: 0
            }
        },
        computed: {
            isInitial() {
                return this.currentStatus === STATUS_INITIAL;
            },
            isSaving() {
                return this.currentStatus === STATUS_SAVING;
            },
            isSuccess() {
                return this.currentStatus === STATUS_SUCCESS;
            },
            isFailed() {
                return this.currentStatus === STATUS_FAILED;
            },
            disabled() {
                return this.isSaving;
            }
        },
        methods: {
            reset() {
                // reset form to initial state
                this.currentStatus = STATUS_INITIAL;
                //this.uploadedFiles = [];
                this.uploadError = null;
                this.fileCount = 0;
            },
            save(formData) {
                var vm = this;
                // upload data to the server
                this.currentStatus = STATUS_SAVING;

                this.$services.upload(formData)
                    .done(files => {
                        if (vm.multiple) {
                            vm.uploadedFiles = vm.uploadedFiles.concat(files);
                        } else {
                            vm.uploadedFiles = files;
                        }
                        vm.currentStatus = STATUS_SUCCESS;

                    })
                    .fail(error => {
                        if (error.responseJSON) {
                            vm.uploadError = error.responseJSON.ExceptionMessage || error.responseJSON.Message || error.responseJSON.error;
                        } else {
                            vm.uploadError = "ERROR!";
                        }
                        vm.currentStatus = STATUS_FAILED;
                    });
            },
            filesChange(fieldName, fileList) {
                // handle file changes
                const formData = new FormData();

                if (!fileList.length) return;

                // append the files to FormData
                Array
                    .from(Array(fileList.length).keys())
                    .map(x => {
                        formData.append(fieldName, fileList[x], fileList[x].name);
                    });

                this.fileCount = fileList.length;

                // save it
                this.save(formData);
            },
            getUrl(path) {
                return this.$services.getResourceUrl(path);
            }
        },
        mounted() {
            //this.reset();
        },
        watch: {
            uploadedFiles() {
                var files = this.uploadedFiles.filter((item) => item.isSuccess && item.filePath).map((item) => item.filePath).join(',');
                this.$emit("fileUploaded", files);
            },
        }
    }
</script>
<style lang="scss">

    .upload-container .dropbox {
        outline: 2px dashed grey; /* the dash box */
        outline-offset: -10px;
        background: lightcyan;
        color: dimgray;
        padding: 10px 10px;
        min-height: 200px; /* minimum height */
        position: relative;
        cursor: pointer;
    }

    .upload-container .input-file {
        opacity: 0; /* invisible but it's there! */
        width: 100%;
        height: 200px;
        position: absolute;
        cursor: pointer;
    }

    .upload-container .dropbox:hover {
        background: lightblue; /* when mouse over to the drop zone, change color */
    }

    .upload-container .dropbox p {
        font-size: 1.2em;
        text-align: center;
        padding: 50px 0;
    }

    .img-wrap {
        position: relative;
        display: inline-block;
        /*border: 1px red solid;*/
        padding: 0;
        font-size: 0;
    }

        .img-wrap .remove-img {
            position: absolute;
            top: 2px;
            right: 2px;
            z-index: 100;
            background-color: #FFF;
            padding: 5px 2px 2px;
            color: #000;
            font-weight: bold;
            cursor: pointer;
            opacity: .2;
            text-align: center;
            font-size: 22px;
            line-height: 10px;
            border-radius: 50%;
        }

        .img-wrap:hover .remove-img {
            opacity: 1;
        }
</style>
