<template>
  <!--<CHeader fixed with-subheader light>
        <CToggler in-header
                  class="ml-3 d-lg-none"
                  @click="$store.commit('toggleSidebarMobile')" />
        <CToggler in-header
                  class="ml-3 d-md-down-none"
                  @click="$store.commit('toggleSidebarDesktop')" />
        <CHeaderBrand class="mx-auto d-lg-none" to="/">-->
  <!--<CIcon name="logo" height="48" alt="Logo" />-->
  <!--</CHeaderBrand>
        <CHeaderNav class="d-md-down-none mr-auto">
        </CHeaderNav>
        <CHeaderNav class="mr-4">
            <template>
                <div class="locale-changer">
                    <CIcon style="width: 16px; height: 16px; padding: 0" v-if="$i18n.locale == 'vi'" name="cif-vn" />
                    <CIcon style="width: 16px; height: 16px; padding: 0" v-if="$i18n.locale == 'en'" name="cif-us" />
                    <select v-model="$i18n.locale" @change="onChangeLocale($event)" style="border: none;">
                        <option v-for="locale in availableLocales" :key="`locale-${locale.value}`" :value="locale.value">{{ locale.text }}</option>
                    </select>
                </div>
            </template>
            <TheHeaderDropdownAccnt />
        </CHeaderNav>
        <CSubheader class="px-3">
            <CBreadcrumbRouter class="border-0 mb-0" />
        </CSubheader>
    </CHeader>-->
  <!-- start: header -->
  <header class="header">
    <div class="logo-container">
      <a href="#" class="logo">
        <img
          src="/assets/img/logo.png"
          width="102"
          height="40"
          alt="ATIN Admin"
        />
      </a>

      <div
        class="d-md-none toggle-sidebar-left"
        data-toggle-class="sidebar-left-opened"
        data-target="html"
        data-fire-event="sidebar-left-opened"
      >
        <i class="fas fa-bars" aria-label="Toggle sidebar"></i>
      </div>
    </div>

    <!-- start: search & user box -->
    <div class="header-right">
      <a
        class="dropdown-language"
        href="#"
        role="button"
        id="dropdownLanguage"
        data-bs-toggle="dropdown"
        aria-haspopup="true"
        aria-expanded="false"
      >
        <img
          src="/assets/img/blank.gif"
          class="flag flag-us"
          alt="English"
          v-if="$i18n.locale === 'en'"
          style="margin-right: 4px"
        />
        <img
          src="/assets/img/blank.gif"
          class="flag flag-vn"
          alt="Tiếng Việt"
          v-if="$i18n.locale === 'vi'"
          style="margin-right: 4px"
        />

        <span>{{ $i18n.locale === "vi" ? " VIE" : " EN" }}</span>
        <i class="fas fa-chevron-down"></i>
      </a>
      <div
        class="dropdown-menu dropdown-menu-right"
        aria-labelledby="dropdownLanguage"
      >
        <a
          class="dropdown-item"
          style="cursor: pointer"
          v-for="localeItem in availableLocales"
          :key="`locale-${localeItem.value}`"
          @click="() => ($i18n.locale = localeItem.value)"
          ><img
            src="/assets/img/blank.gif"
            :class="localeItem.value === 'en' ? 'flag flag-us' : 'flag flag-vn'"
            :alt="localeItem.text"
          />
          {{ localeItem.text }}</a
        >
      </div>
      <span class="separator"></span>

      <ul class="notifications">
        <li>
          <a
            href="#"
            class="dropdown-toggle notification-icon"
            data-bs-toggle="dropdown"
          >
            <i class="bx bx-list-ol"></i>
            <span class="badge">3</span>
          </a>

          <div class="dropdown-menu notification-menu large">
            <div class="notification-title">
              <span class="float-end badge badge-default">3</span>
              Tasks
            </div>

            <div class="content">
              <ul>
                <li>
                  <p class="clearfix mb-1">
                    <span class="message float-start"
                      >Generating Sales Report</span
                    >
                    <span class="message float-end text-dark">60%</span>
                  </p>
                  <div class="progress progress-xs light">
                    <div
                      class="progress-bar"
                      role="progressbar"
                      aria-valuenow="60"
                      aria-valuemin="0"
                      aria-valuemax="100"
                      style="width: 60%"
                    ></div>
                  </div>
                </li>

                <li>
                  <p class="clearfix mb-1">
                    <span class="message float-start">Importing Contacts</span>
                    <span class="message float-end text-dark">98%</span>
                  </p>
                  <div class="progress progress-xs light">
                    <div
                      class="progress-bar"
                      role="progressbar"
                      aria-valuenow="98"
                      aria-valuemin="0"
                      aria-valuemax="100"
                      style="width: 98%"
                    ></div>
                  </div>
                </li>

                <li>
                  <p class="clearfix mb-1">
                    <span class="message float-start"
                      >Uploading something big</span
                    >
                    <span class="message float-end text-dark">33%</span>
                  </p>
                  <div class="progress progress-xs light mb-1">
                    <div
                      class="progress-bar"
                      role="progressbar"
                      aria-valuenow="33"
                      aria-valuemin="0"
                      aria-valuemax="100"
                      style="width: 33%"
                    ></div>
                  </div>
                </li>
              </ul>
            </div>
          </div>
        </li>
        <li>
          <a
            href="#"
            class="dropdown-toggle notification-icon"
            data-bs-toggle="dropdown"
          >
            <i class="bx bx-envelope"></i>
            <span class="badge">4</span>
          </a>

          <div class="dropdown-menu notification-menu">
            <div class="notification-title">
              <span class="float-end badge badge-default">230</span>
              Messages
            </div>

            <div class="content">
              <ul>
                <li>
                  <a href="#" class="clearfix">
                    <figure class="image">
                      <img
                        src="/assets/img/%21sample-user.jpg"
                        alt="Joseph Doe Junior"
                        class="rounded-circle"
                      />
                    </figure>
                    <span class="title">Joseph Doe</span>
                    <span class="message">Lorem ipsum dolor sit.</span>
                  </a>
                </li>
                <li>
                  <a href="#" class="clearfix">
                    <figure class="image">
                      <img
                        src="/assets/img/%21sample-user.jpg"
                        alt="Joseph Junior"
                        class="rounded-circle"
                      />
                    </figure>
                    <span class="title">Joseph Junior</span>
                    <span class="message truncate"
                      >Truncated message. Lorem ipsum dolor sit amet,
                      consectetur adipiscing elit. Donec sit amet lacinia orci.
                      Proin vestibulum eget risus non luctus. Nunc cursus
                      lacinia lacinia. Nulla molestie malesuada est ac
                      tincidunt. Quisque eget convallis diam, nec venenatis
                      risus. Vestibulum blandit faucibus est et malesuada. Sed
                      interdum cursus dui nec venenatis. Pellentesque non nisi
                      lobortis, rutrum eros ut, convallis nisi. Sed tellus
                      turpis, dignissim sit amet tristique quis, pretium id est.
                      Sed aliquam diam diam, sit amet faucibus tellus ultricies
                      eu. Aliquam lacinia nibh a metus bibendum, eu commodo eros
                      commodo. Sed commodo molestie elit, a molestie lacus
                      porttitor id. Donec facilisis varius sapien, ac fringilla
                      velit porttitor et. Nam tincidunt gravida dui, sed
                      pharetra odio pharetra nec. Duis consectetur venenatis
                      pharetra. Vestibulum egestas nisi quis elementum
                      elementum.</span
                    >
                  </a>
                </li>
                <li>
                  <a href="#" class="clearfix">
                    <figure class="image">
                      <img
                        src="/assets/img/%21sample-user.jpg"
                        alt="Joe Junior"
                        class="rounded-circle"
                      />
                    </figure>
                    <span class="title">Joe Junior</span>
                    <span class="message">Lorem ipsum dolor sit.</span>
                  </a>
                </li>
                <li>
                  <a href="#" class="clearfix">
                    <figure class="image">
                      <img
                        src="/assets/img/%21sample-user.jpg"
                        alt="Joseph Junior"
                        class="rounded-circle"
                      />
                    </figure>
                    <span class="title">Joseph Junior</span>
                    <span class="message"
                      >Lorem ipsum dolor sit amet, consectetur adipiscing elit.
                      Donec sit amet lacinia orci. Proin vestibulum eget risus
                      non luctus. Nunc cursus lacinia lacinia. Nulla molestie
                      malesuada est ac tincidunt. Quisque eget convallis
                      diam.</span
                    >
                  </a>
                </li>
              </ul>

              <hr />

              <div class="text-end">
                <a href="#" class="view-more">View All</a>
              </div>
            </div>
          </div>
        </li>
        <li>
          <a
            href="#"
            class="dropdown-toggle notification-icon"
            data-bs-toggle="dropdown"
          >
            <i class="bx bx-bell"></i>
            <span class="badge">3</span>
          </a>

          <div class="dropdown-menu notification-menu">
            <div class="notification-title">
              <span class="float-end badge badge-default">3</span>
              Alerts
            </div>

            <div class="content">
              <ul>
                <li>
                  <a href="#" class="clearfix">
                    <div class="image">
                      <i class="fas fa-thumbs-down bg-danger text-light"></i>
                    </div>
                    <span class="title">Server is Down!</span>
                    <span class="message">Just now</span>
                  </a>
                </li>
                <li>
                  <a href="#" class="clearfix">
                    <div class="image">
                      <i class="bx bx-lock bg-warning text-light"></i>
                    </div>
                    <span class="title">User Locked</span>
                    <span class="message">15 minutes ago</span>
                  </a>
                </li>
                <li>
                  <a href="#" class="clearfix">
                    <div class="image">
                      <i class="fas fa-signal bg-success text-light"></i>
                    </div>
                    <span class="title">Connection Restaured</span>
                    <span class="message">10/10/2021</span>
                  </a>
                </li>
              </ul>

              <hr />

              <div class="text-end">
                <a href="#" class="view-more">View All</a>
              </div>
            </div>
          </div>
        </li>
      </ul>

      <span class="separator"></span>

      <div id="userbox" class="userbox">
        <a href="#" data-bs-toggle="dropdown">
          <figure class="profile-picture">
            <img
              src="/assets/img/%21logged-user.jpg"
              alt="Joseph Doe"
              class="rounded-circle"
              data-lock-picture="img/%21logged-user.jpg"
            />
          </figure>
          <div
            class="profile-info"
            data-lock-name="John Doe"
            data-lock-email="johndoe@okler.com"
          >
            <span class="name">{{ fullName }}</span>
            <span class="role">{{ user.companyName }}</span>
          </div>

          <i class="fa custom-caret"></i>
        </a>

        <div class="dropdown-menu">
          <ul class="list-unstyled mb-2">
            <li class="divider"></li>
            <li>
              <router-link
                :to="{ path: `/personal/${userId}` }"
                role="menuitem"
              >
                <i class="bx bx-user-circle"></i
                >{{ $t("Header.ChangePassword") }}
              </router-link>
            </li>
            <!-- <li>
              <a role="menuitem" tabindex="-1" href="pages-user-profile.html"
                ><i class="bx bx-user-circle"></i> My Profile</a
              >
            </li> -->
            <!-- <li>
              <a role="menuitem" tabindex="-1" href="#" data-lock-screen="true"
                ><i class="bx bx-lock"></i> Lock Screen</a
              >
            </li> -->
            <li>
              <a
                role="menuitem"
                tabindex="-1"
                @click.prevent="logout"
                style="cursor: pointer"
                ><i class="bx bx-power-off"></i> Logout</a
              >
            </li>
          </ul>
        </div>
      </div>
    </div>
    <!-- end: search & user box -->
  </header>
  <!-- end: header -->
</template>

<script>
//import TheHeaderDropdownAccnt from './TheHeaderDropdownAccnt'
import i18n from "@/libs/i18n";
import Services from "@/utils/services";

export default {
  name: "TheHeader",
  components: {
    //TheHeaderDropdownAccnt
  },
  data() {
    return {
      availableLocales: [],
      user: {
        fullname: null,
        compId: null,
        company: null,
        userId: null,
      },
      userId: 1,
      // fullName: null,
    };
  },
  mounted() {
    console.log("123");
    var accessToken = Services.getAccessToken();

    this.userId = accessToken.userId;
    var userid = accessToken.userId;
    if (userid) {
      this.loadUserDetail(userid);
    }
  },
  computed: {
    locale: function () {
      return i18n.locale;
    },
    fullName: function () {
      return Services.getAccessToken().fullName;
    },
  },
  watch: {
    locale: function (newValue, oldValue) {
      var vm = this;
      //get old value
      let localeCache = localStorage.getItem("lang");
      if (newValue != localeCache) {
        localStorage.setItem("lang", newValue);
        vm.$router.go();
      }
    },
  },
  methods: {
    notificationSystemCount() {
      return this.$services.get(`notificationSystem/count`).done((count) => {
        this.$store.commit("setTotalNotifications", count);
      });
    },
    loadUserDetail(userId) {
      var vm = this;
      return this.$services.get(`users/get-self-info`).done((user) => {
        vm.user = user;
        console.log(vm.user);
      });
    },
    generateLocales() {
      for (var i = 0; i < i18n.availableLocales.length; i++) {
        var locale = i18n.availableLocales[i];
        let item = {
          value: locale,
          text: locale,
        };
        if (locale == "vi") {
          item.text = "Tiếng Việt";
        } else if (locale == "en") {
          item.text = "English";
        }
        this.availableLocales.push(item);
      }
    },
    logout() {
      var vm = this;
      this.$store.dispatch("deauthenticate");
      vm.$router.push({ path: "/login" });
    },

    onChangeLocale(newLocale) {},
  },
  created() {
    this.generateLocales();
  },
};
</script>
