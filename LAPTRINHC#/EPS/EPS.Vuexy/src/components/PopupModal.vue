<template>
  <div v-if="modalOpen">
    <CModal
      :show.sync="modalOpen"
      :no-close-on-backdrop="true"
      :title="title"
      size="lg"
      color="dark"
    >
      <b-form class="form-horizontal">
        <b-row>
          <b-col md="12">
            <div>
              <b-col md="12">
                <slot></slot>
              </b-col>
            </div>
          </b-col>
        </b-row>
      </b-form>
      <template #footer>
        <b-button variant="primary" @click="confirm">
          <i class="fa fa-floppy-o"></i> {{ $t("Button.Yes") }}
        </b-button>
        <b-button
          type="button"
          variant="secondary"
          @click="close"
          style="width: 120px"
        >
          <i class="fa fa-ban"></i> {{ $t("Button.No") }}</b-button
        >
      </template>
    </CModal>
  </div>
</template>
<script>
export default {
  name: "PopupModal",
  props: {
    title: {
      type: String,
      require: true,
    },
  },
  data() {
    return {
      modalOpen: false,
    };
  },
  created() {},
  methods: {
    open() {
      this.modalOpen = true;
    },

    close() {
      this.modalOpen = false;
    },
    confirm() {
      this.$emit("confirm-modal");
    },
  },
};
</script>
<style></style>
