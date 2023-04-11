<template>
    <div>
        <textarea class="form-control"></textarea>
    </div>
</template>
<script>
    import 'bootstrap/dist/js/bootstrap.bundle'
    import 'codemirror/lib/codemirror.css';
    import 'codemirror/theme/monokai.css';
    import js from 'codemirror/mode/javascript/javascript';
    require('summernote/dist/summernote-bs4.css')
    require('summernote/dist/summernote-bs4.js')

    export default {
        name: 'summernote',
        model: {
            prop: 'text',
            event: 'change'
        },
        props: {
            text: {
                required: true
            },
            height: {
                type: String,
                default: '500'
            }
        },
        mounted() {
            let config = {
                height: this.height,
                codemirror: { // codemirror options
                    theme: 'monokai'
                }
            };
            let vm = this;
            config.callbacks = {
                onInit() {
                    $(vm.$el).summernote("code", vm.text);
                },
                onChange() {
                    vm.$emit('change', $(vm.$el).summernote('code'));
                },
                //onImageUpload: function (files) {
                //    var formData = new FormData();
                //    formData.append('files', files[0], files[0].name)

                //    vm.$services.upload(formData)
                //        .done(files => {
                //            let url = vm.$services.getResourceUrl(files[0].filePath) // Get url from response

                //            var image = $('<img>').attr('src', url);
                //            $(vm.$el).summernote("insertNode", image[0]);
                //        })
                //        .fail(error => {
                //            if (error.responseJSON) {
                //                alert(error.responseJSON.ExceptionMessage || error.responseJSON.Message || error.responseJSON.error)
                //            } else {
                //                alert(error);
                //            }
                //        });
                //}
            };
            $(this.$el).summernote(config);
        },
        beforeDestroy() {
            $(this.$el).summernote('destroy');
        }
    };
</script>
