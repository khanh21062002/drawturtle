<template>
    <div class="animated fadeIn">
        <ckeditor :editor="editor" :config="editorConfig"
                  v-model="htmlValue">
        </ckeditor>
    </div>
</template>
<script>
    
    import ClassicEditorBase from '@ckeditor/ckeditor5-editor-classic/src/classiceditor';
    import Essentials from '@ckeditor/ckeditor5-essentials/src/essentials';
    import Font from '@ckeditor/ckeditor5-font/src/font';
    import Alignment from '@ckeditor/ckeditor5-alignment/src/alignment';
    import UploadAdapter from '@ckeditor/ckeditor5-adapter-ckfinder/src/uploadadapter';
    import Autoformat from '@ckeditor/ckeditor5-autoformat/src/autoformat';
    import Bold from '@ckeditor/ckeditor5-basic-styles/src/bold';
    import Italic from '@ckeditor/ckeditor5-basic-styles/src/italic';
    import BlockQuote from '@ckeditor/ckeditor5-block-quote/src/blockquote';
    import Heading from '@ckeditor/ckeditor5-heading/src/heading';
    import Image from '@ckeditor/ckeditor5-image/src/image';
    import ImageCaption from '@ckeditor/ckeditor5-image/src/imagecaption';
    import ImageStyle from '@ckeditor/ckeditor5-image/src/imagestyle';
    import ImageToolbar from '@ckeditor/ckeditor5-image/src/imagetoolbar';
    import ImageUpload from '@ckeditor/ckeditor5-image/src/imageupload';
    import Indent from '@ckeditor/ckeditor5-indent/src/indent';
    import Link from '@ckeditor/ckeditor5-link/src/link';
    import List from '@ckeditor/ckeditor5-list/src/list';
    import MediaEmbed from '@ckeditor/ckeditor5-media-embed/src/mediaembed';
    import Paragraph from '@ckeditor/ckeditor5-paragraph/src/paragraph';
    import PasteFromOffice from '@ckeditor/ckeditor5-paste-from-office/src/pastefromoffice';
    import Table from '@ckeditor/ckeditor5-table/src/table';
    import TableToolbar from '@ckeditor/ckeditor5-table/src/tabletoolbar';
    import TextTransformation from '@ckeditor/ckeditor5-typing/src/texttransformation';
    import ImageResizeEditing from '@ckeditor/ckeditor5-image/src/imageresize/imageresizeediting';
    import ImageResizeHandles from '@ckeditor/ckeditor5-image/src/imageresize/imageresizehandles';
    import Base64UploadAdapter from '@ckeditor/ckeditor5-upload/src/adapters/base64uploadadapter';


    export default {
        name: 'app-ckeditor',
        props: {
            value: {
            },
        },
       
        data(){
            return {
                editor: ClassicEditorBase,
                editorConfig: {
                    plugins: [
                        Essentials,
                        Font,
                        Alignment,
                        UploadAdapter,
                        Autoformat,
                        Bold,
                        Italic,
                        BlockQuote,
                        Heading,
                        Image,
                        ImageCaption,
                        ImageStyle,
                        ImageToolbar,
                        ImageUpload,
                        Indent,
                        Link,
                        List,
                        MediaEmbed,
                        Paragraph,
                        PasteFromOffice,
                        Table,
                        TableToolbar,
                        TextTransformation,
                        ImageResizeEditing,
                        ImageResizeHandles,
                        Base64UploadAdapter
                    ],
                    fontSize: {
                        options: [
                            9,
                            11,
                            13,
                            14,
                            15,
                            16,
                            17,
                            19,
                            20,
                            22,
                            23,
                            25,
                        ],
                    },
                    fontFamily: {
                        options: [
                            'default',
                            'Arial, Helvetica, sans-serif',
                            'Courier New, Courier, monospace',
                            'Georgia, serif',
                            'Lucida Sans Unicode, Lucida Grande, sans-serif',
                            'Tahoma, Geneva, sans-serif',
                            'Times New Roman, Times, serif',
                            'Trebuchet MS, Helvetica, sans-serif',
                            'Verdana, Geneva, sans-serif'
                        ]
                    },
                    mediaEmbed: {
                        previewsInData: true,
                    },
                    toolbar: {
                        items: [
                            'heading',
                            '|',
                            'fontSize',
                            'fontFamily', '|',
                            'alignment', '|',
                            'fontColor',
                            'fontBackgroundColor',
                            '|',
                            'bold',
                            'italic',
                            'link',
                            'bulletedList',
                            'numberedList',
                            '|',
                            'indent',
                            'outdent',
                            '|',
                            'imageUpload',
                            'blockQuote',
                            'insertTable',
                            'mediaEmbed',
                            'undo'
                        ]
                    },
                    image: {
                        toolbar: [
                            'imageStyle:block',
                            'imageStyle:side',
                            '|',
                            'imageTextAlternative'
                        ]
                    },
                    table: {
                        contentToolbar: [
                            'tableColumn',
                            'tableRow',
                            'mergeTableCells'
                        ]
                    },
                },
                htmlValue: null,
            }
        },
        created() {
        },
        methods: {
        },
        watch: {
            htmlValue() {
                this.$emit('input', this.htmlValue);
            },
            value(content) {
                this.htmlValue = content;
            }
        }
    }
</script>
<style>
</style>
