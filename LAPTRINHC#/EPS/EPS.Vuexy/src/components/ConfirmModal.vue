<template>
  <div>
    <popupModal ref="popup" @confirm-modal="confirm" :title="title">
      <label>{{ message }}</label>
    </popupModal>
  </div>
</template>
<script>
import popupModal from "@/components/PopupModal.vue";

export default {
  name: "ConfirmModal",
  components: {
    PopupModal: popupModal,
  },
  data: () => ({
    title: null,
    message: null,
  }),
  created() {},
  methods: {
    show(opts = {}) {
      this.title = opts.title;
      this.message = opts.message;
      this.$refs.popup.open();
      return new Promise((resolve, reject) => {
        this.resolvePromise = resolve;
        this.rejectPromise = reject;
      });
    },

    confirm() {
      this.$refs.popup.close();
      this.resolvePromise(true);
    },
  },
};
</script>
<style></style>
