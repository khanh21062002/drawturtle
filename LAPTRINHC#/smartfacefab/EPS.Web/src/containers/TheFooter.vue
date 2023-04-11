<template>
  <CFooter :fixed="false">
    <div>
        <a :href="links" target="_blank">{{footer}}</a>
      <span class="ml-1">&copy; {{new Date().getFullYear()}} <!--creativeLabs--></span>
    </div>
    <div class="ml-auto">
      <!--<span class="mr-1">Powered by</span>
      <a href="https://coreui.io/vue" target="_blank">CoreUI for Vue</a>-->
    </div>
  </CFooter>
</template>

<script>

export default {
        name: 'TheFooter',
       
        data() {
            return {
                footer: "",
                links: "",
            };
        },
        created() {
            var vm = this;
            //read config
            $.getJSON('brandconfig.json', function (data) {
                if (data && data.Company && data.Company.Footer) {
                    vm.footer = data.Company.Footer;

                }
                if (data && data.Company && data.Company.Links) {
                    vm.links = data.Company.Links;
                }

            });

        },

}
</script>
