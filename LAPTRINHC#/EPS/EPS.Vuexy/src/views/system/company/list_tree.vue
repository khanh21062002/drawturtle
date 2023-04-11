<template>
  <div class="row">
    <div class="col">
      <section class="card">
        <header class="card-header">
          <strong class="card-title">{{
            $t("System.Company.List.Header")
          }}</strong>
        </header>
        <div class="card-body">
          <vue-ads-table-tree
            :columns="columns"
            :rows="rows"
            :page="page"
            :styling="styles"
            @page-change="pageChanged"
            @selection-change="selectionChanged"
            ref="treeTable"
          >
            <template slot="top" slot-scope="topper">
              <b-row>
                <b-col
                  md="6"
                  class="text-left"
                  style="margin-right: 18px; margin-left: 3px"
                >
                  <b-form-group
                    :label="$t('System.Company.List.SearchForm.CompanyName')"
                    label-for="companyFilter"
                    :label-cols="5"
                    :horizontal="true"
                    label-align-md="left"
                    class="form-group row pb-4"
                    label-class="col-lg-3 control-label text-lg-start pt-2"
                  >
                    <b-form-input
                      type="tel"
                      :placeholder="$t('PlaceHolder.SearchInput')"
                      id="companyFilter"
                      maxlength="20"
                      v-model="topper.filter"
                      @change="topper.filterChanged"
                    >
                    </b-form-input>
                  </b-form-group>
                </b-col>
              </b-row>
              <b-row
                ><b-col md="6" offset="5">
                  <b-form-group label="" :label-cols="1">
                    <b-button type="submit" variant="primary">{{
                      $t("Button.Search")
                    }}</b-button>
                  </b-form-group>
                </b-col>
              </b-row>
              <b-row
                class="grid-border-top"
                style="margin-left: 0px; margin-right: 0px"
              >
                <b-col
                  md="4"
                  class="text-left"
                  :label-cols="5"
                  style="
                    text-align: left;
                    margin-bottom: 5px;
                    margin-left: -14px;
                  "
                >
                  <h4>{{ $t("System.Company.List.Header") }}</h4>
                </b-col>
                <b-col
                  md="8"
                  class="text-right"
                  :label-cols="12"
                  style="
                    text-align: right;
                    margin-bottom: 5px;
                    margin-right: -26px;
                    margin-left: 34px;
                  "
                >
                  <router-link
                    to="/system/company/create/"
                    tag="button"
                    class="btn btn-primary"
                    size="md"
                  >
                    {{ $t("Button.Create") }}
                  </router-link>
                </b-col>
              </b-row>
            </template>
            <template slot="index" slot-scope="props">
              {{ props.row._meta.index + 1 }}
            </template>
            <template slot="action" slot-scope="props">
              <b-button
                size="sm"
                :to="{ path: `/system/company/detail/${props.row.id}` }"
                title="Xem chi tiết"
              >
                <i class="fa fa-eye" aria-hidden="true"></i>
              </b-button>
              <b-button
                size="sm"
                @click.stop="remove(props.row, $event.target)"
                class="mr-1"
                title="Xóa đơn vị"
              >
                <i class="fa fa-trash-o" aria-hidden="true"></i>
              </b-button>
              <!--<b-button size="sm" :to="{ path: `/system/company/create/${props.row.id}` }" title="Thêm đơn vị trực thuộc" >
                                    <i class="fa fa-plus" aria-hidden="true"></i>
                                </b-button>-->
            </template>
          </vue-ads-table-tree>
        </div>
      </section>
      <confirmModal ref="confirmModal"> </confirmModal>
    </div>
  </div>
</template>

<script>
import i18n from "@/libs/i18n";

export default {
  name: "TableContainerApp",

  data() {
    let rows = [];

    let columns = [
      {
        property: "action",
        direction: null,
        filterable: false,
        groupable: false,
      },
      {
        property: "index",
        title: this.$t("System.Company.List.Table.Index"),
        direction: null,
        filterable: false,
        groupable: false,
        collapseIcon: true,
      },
      {
        property: "code",
        title: this.$t("System.Company.List.Table.Code"),
        direction: null,
        filterable: true,
        groupable: false,
      },
      {
        property: "label",
        title: this.$t("System.Company.List.Table.Name"),
        direction: null,
        filterable: true,
        groupable: false,
        groupCollapsable: false,
        hideOnGroup: true,
      },
    ];
    let styles = {
      group: {
        "vue-ads-font-bold": true,
        "vue-ads-border-b": true,
        "vue-ads-italic": true,
      },
      "0/all": {
        "vue-ads-py-2": true,
        "vue-ads-px-1": true,
      },
      "even/": {
        "vue-ads-bg-blue-lighter": true,
      },
      "odd/": {
        "vue-ads-bg-blue-lightest": true,
      },
      "0/": {
        "vue-ads-bg-blue-lighter": false,
        "vue-ads-bg-blue-dark": true,
        "vue-ads-text-white": true,
        "vue-ads-font-bold": true,
      },
      "1_/": {
        "hover:vue-ads-bg-red-lighter": true,
      },
      "1_/0": {
        leftAlign: true,
      },
    };

    return {
      rows,
      columns,
      styles,
      page: 0,
      filter: "",
      selectedRowIds: [],
    };
  },
  created() {},
  computed: {
    nothingSelected() {
      return this.selectedRowIds.length === 0;
    },
  },

  methods: {
    sleep(ms) {
      return new Promise((resolve) => setTimeout(resolve, ms));
    },

    filterChanged(filter) {
      let filterTrim = "";
      if (filter) filterTrim = filter.trim();
      this.filter = filterTrim;
    },

    pageChanged(page) {
      this.page = page;
    },

    selectionChanged(selectedRows) {
      this.selectedRowIds = selectedRows.map((row) => row.id);
    },

    async callTempRows() {
      await this.sleep(1000);

      return {
        rows: [],
        total: 0,
      };
    },

    deleteRows() {},

    async addRows() {
      var vm = this;
      return this.$services.get(`lookup/company-tree`).done((lstcompany) => {
        vm.rows = lstcompany;
      });
    },
    async remove(item, button) {
      var vm = this;
      const ok = await this.$refs.confirmModal.show({
        title: this.$t("Button.Delete"),
        message: this.$t("Messages.ConfirmDelete"),
      });
      // If you throw an error, the method will terminate here unless you surround it wil try/catch
      if (ok) {
        this.$services.delete(`company/${item.id}`).done(() => {
          vm.$toastr.s(i18n.t("Messages.DeleteSuccess"));
          vm.addRows();
        });
      } else {
        this.$refs.popup.close();
      }
    },
  },

  mounted() {
    // Add rows after component is created to illustrate
    // how the change is recognized and reflected in the table.
    this.addRows();
  },
};
</script>
<style>
tbody {
  text-align: left !important;
}

.fa-sort {
  display: none;
}

thead {
  background: aliceblue;
}

th.vue-ads-cursor-pointer.vue-ads-border-r.vue-ads-px-4.vue-ads-py-2.vue-ads-text-sm.vue-ads-text-left:nth-child(
    1
  ) {
  width: 141px;
}

th.vue-ads-cursor-pointer.vue-ads-border-r.vue-ads-px-4.vue-ads-py-2.vue-ads-text-sm.vue-ads-text-left:nth-child(
    2
  ) {
  width: 141px;
}

tr.hover\:vue-ads-bg-gray-200.vue-ads-bg-white.vue-ads-border-b {
  background-color: #f2f2f2;
}

tr.hover\:vue-ads-bg-gray-200.vue-ads-bg-gray-100.vue-ads-border-b {
  background-color: #f5f6f775;
}
</style>
