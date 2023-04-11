<template>
    <select>
        <slot></slot>
    </select>
</template>
<script>
    export default {
        name: 'select2',
        props: ['options', 'value'],
        data() {
            return {
                isChanging: false
            }
        },
        mounted: function () {
            var vm = this;

            $(this.$el)
                // init select2
                .select2(this.getSelect2Options(this.options))
                .val(this.value)
                .trigger('change')
                // emit event on change.
                .on('change', function () {
                    if (!vm.isChanging) {
                        vm.isChanging = true;
                        vm.$emit('input', $(this).val())
                        vm.$nextTick(function () {
                            vm.isChanging = false;
                        });
                    }
                })
        },
        methods: {
            getSelect2Options(data) {
                return {
                    data: data,
                    closeOnSelect: !this.$el.hasAttribute('multiple'),
                    allowClear: this.$el.hasAttribute('allowClear'),
                    placeholder: this.$el.getAttribute('placeholder')
                };
            }
        },
        watch: {
            value: function (value) {
                // update value
                if (!this.isChanging) {
                    this.isChanging = true;
                    $(this.$el)
                        .val(value)
                        .trigger('change');
                    this.isChanging = false;
                }
            },
            options: function (options) {
                // update options
                $(this.$el).empty().select2(this.getSelect2Options(options));
                $(this.$el).val(null).trigger('change');
            }
        },
        destroyed: function () {
            $(this.$el).off().select2('destroy')
        }
    }
</script>

