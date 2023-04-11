<template>
    <div class="animated fadeIn">
        <b-row id="pie-chart">
            <b-col md="4">
                <b-card style="height: 450px;">
                    <b-row>
                        <b-col>
                            <h4 class="card-title mb-0"> Tổng hợp thông tin đối tượng </h4>
                        </b-col>
                    </b-row>
                    <!--<b-row> {{$t('Nav.Report')}} </b-row>-->
                    <b-row style="padding-top: 20px; justify-content: center; font-weight: bold">
                        <div style="margin-right: 10px; padding-top: 10px; padding: 20px 15px; background-color: #c5e0b4; border-radius: 20px; display: flex; align-items: center; flex-flow: column; width: 130px; ">
                            <div style="margin-bottom: 10px;">
                                Đang làm việc
                            </div>
                            <div style="padding: 10px 15px; background-color: #ffd78f; border-radius: 50px; width: 100px; text-align: center; ">
                                {{ empWorking }}
                            </div>
                        </div>
                        <div style="padding-top: 10px; padding: 20px 15px; background-color: #fff2cc; border-radius: 20px; display: flex; align-items: center; flex-flow: column; width: 130px; ">
                            <div style="margin-bottom: 10px;">
                                Nghỉ việc
                            </div>
                            <div style="padding: 10px 15px; background-color: #9cadda; border-radius: 50px; width: 100px; text-align: center; ">
                                {{ empInactivity }}
                            </div>
                        </div>
                    </b-row>
                    <b-row style="padding-top: 10px; justify-content: center; font-weight: bold">
                        <div style="margin-right: 10px; padding-top: 10px; padding: 20px 15px; background-color: #f8cbad; border-radius: 20px; display: flex; align-items: center; flex-flow: column; width: 130px; ">
                            <div style="margin-bottom: 10px;">
                                Blacklist
                            </div>
                            <div style="padding: 10px 15px; background-color: #ff0000; color: white; border-radius: 50px; width: 100px; text-align: center; ">
                                {{ blacklist }}
                            </div>
                        </div>
                        <div style="padding-top: 10px; padding: 20px 15px; background-color: #deebf7; border-radius: 20px; display: flex; align-items: center; flex-flow: column; width: 130px; ">
                            <div style="margin-bottom: 10px;">
                                Chưa đăng ký
                            </div>
                            <div style="padding: 10px 15px; background-color: #a5cd93; border-radius: 50px; width: 100px; text-align: center; ">
                                {{ unregistered }}
                            </div>
                        </div>
                    </b-row>
                </b-card>
            </b-col>
            <b-col md="8">
                <b-card style="height: 450px;">
                    <b-row>
                        <b-col sm="7">
                            <h4 class="card-title mb-0"> Thống kê ra vào nhà máy </h4>
                        </b-col>
                        <b-col class="text-right">
                            <b-button size="md" style="width: 80px !important" @click="loadStatisticPersonInOut(7)" class="btn btn-danger">
                                7 ngày
                            </b-button>
                            <b-button size="md" style="width: 80px !important" @click="loadStatisticPersonInOut(10)" class="btn btn-info">
                                10 ngày
                            </b-button>
                            <b-button size="md" style="width: 80px !important" @click="loadStatisticPersonInOut(15)" class="btn btn-primary">
                                15 ngày
                            </b-button>
                        </b-col>
                    </b-row>
                    <div>
                        <apexchart type="bar"
                                   height="350"
                                   ref="columnChartPersonInOut"
                                   :options="columnChartPersonInOut.chartOptions"
                                   :series="columnChartPersonInOut.series">
                        </apexchart>
                    </div>
                </b-card>
            </b-col>
        </b-row>
        <b-row id="pie-chart">
            <b-col md="6">
                <b-card style="height: 475px;">
                    <b-row>
                        <b-col sm="7">
                            <h4 class="card-title mb-0"> Thống kê đối tượng chưa đăng ký ra vào </h4>
                        </b-col>
                        <b-col class="text-right">
                            <b-button size="md" style="width: 80px !important" @click="loadStatisticUnregistered(7)" class="btn btn-danger">
                                7 ngày
                            </b-button>
                            <b-button size="md" style="width: 80px !important" @click="loadStatisticUnregistered(10)" class="btn btn-info">
                                10 ngày
                            </b-button>
                            <b-button size="md" style="width: 80px !important" @click="loadStatisticUnregistered(15)" class="btn btn-primary">
                                15 ngày
                            </b-button>
                        </b-col>
                    </b-row>
                    <div>
                        <apexchart type="bar"
                                   height="350"
                                   ref="columnChartUnregistered"
                                   :options="columnChartUnregistered.chartOptions"
                                   :series="columnChartUnregistered.series">
                        </apexchart>
                    </div>
                </b-card>
            </b-col>
            <b-col md="6">
                <b-card style="height: 475px;">
                    <b-row style="padding-bottom: 15px">
                        <b-col sm="7">
                            <h4 class="card-title mb-0"> 10 nhân viên ra vào nhà máy nhiều nhất </h4>
                        </b-col>
                        <b-col class="text-right">
                            <b-button size="md" style="width: 80px !important" @click="refreshTopTable(3)" class="btn btn-danger">
                                3 ngày
                            </b-button>
                            <b-button size="md" style="width: 80px !important" @click="refreshTopTable(7)" class="btn btn-info">
                                7 ngày
                            </b-button>
                            <b-button size="md" style="width: 80px !important" @click="refreshTopTable(10)" class="btn btn-primary">
                                10 ngày
                            </b-button>
                        </b-col>
                    </b-row>
                    <b-row style="overflow: hidden; overflow-y: scroll; height: 325px;">
                        <b-grid :gridOptions="gridOptions"
                                :footerable="false"
                                :actionable="false"
                                :dataUrl="dataURL"
                                gridName=""
                                ref="topTable">
                            <template v-slot:actions>
                            </template>
                        </b-grid>
                    </b-row>
                </b-card>
            </b-col>
        </b-row>
    </div>
</template>
<script>
    import moment from 'moment';
    import i18n from '@/libs/i18n'

    export default {
        name: 'home',
        data: function () {
            return {
                empWorking: null,
                empInactivity: null,
                blacklist: null,
                unregistered: null,
                columnChartPersonInOut: {
                    selection: '7_days',
                    series: [
                        {
                            name: "Nhân viên",
                            data: [],
                        },
                        {
                            name: "Tổng",
                            data: []
                        },
                    ],
                    chartOptions: {
                        chart: {
                            height: 350,
                            type: 'bar',
                            zoom: {
                                enabled: false,
                            },
                            stacked: false,
                            toolbar: {
                                show: false,
                                tools: {
                                    download: false // <== line to add
                                }
                            }
                        },
                        plotOptions: {
                            bar: {
                                dataLabels: {
                                    position: 'center', // top, center, bottom
                                },
                            }
                        },
                        dataLabels: {
                            enabled: true,
                            formatter: function (val) {
                                if (val > 0) {
                                    return val + "";
                                } else {
                                    return '';
                                }
                            },
                            style: {
                                fontSize: '11px',
                                colors: ["#304758"]
                            }
                        },
                        xaxis: {
                            categories: [],
                            type: 'datetime',
                            labels: {
                                format: 'dd/MM/yyyy',
                            },
                            axisBorder: {
                                show: false
                            },
                            axisTicks: {
                                show: false
                            },
                            title: {
                                text: i18n.t('Home.ColumnChart.Day'),
                                style: {
                                    fontSize: '15px',
                                    fontFamily: 'san-serif'
                                }
                            },
                            crosshairs: {
                                fill: {
                                    type: 'gradient',
                                    gradient: {
                                        colorFrom: '#D8E3F0',
                                        colorTo: '#BED1E6',
                                        stops: [0, 100],
                                        opacityFrom: 0.4,
                                        opacityTo: 0.5,
                                    }
                                }
                            },
                            tooltip: {
                                enabled: true,
                            }
                        },
                        responsive: [{
                            breakpoint: 480,
                            options: {
                                legend: {
                                    position: 'bottom',
                                    offsetX: -10,
                                    offsetY: 0
                                }
                            }
                        }],
                        yaxis: {
                            axisBorder: {
                                show: false
                            },
                            axisTicks: {
                                show: false,
                            },
                            labels: {
                                show: true,
                                formatter: function (val) {
                                    return val;
                                }
                            },
                            title: {
                                text: i18n.t('Home.ColumnChart.Count'),
                                style: {
                                    fontSize: '15px',
                                    fontFamily: 'san-serif'
                                }
                            },
                        },
                        tooltip: {
                            x: {
                                //show: true,
                                //format: 'dd/MM/yyyy HH:mm:ss',
                                formatter: (value) => {
                                    return moment(value).zone('+0700').format('DD/MM/YYYY')
                                },
                               
                            },
                        },
                    },
                },
                columnChartUnregistered: {
                    date: moment().subtract(1, 'days').format('YYYY-MM-DD'),
                    selection: '7_days',
                    series: [
                        {
                            name: "Chưa đăng ký",
                            data: [],
                        },
                    ],
                    chartOptions: {
                        chart: {
                            height: 350,
                            type: 'bar',
                            zoom: {
                                enabled: false,
                            },
                            stacked: false,
                            toolbar: {
                                show: false,
                                tools: {
                                    download: false // <== line to add
                                }
                            }
                        },
                        plotOptions: {
                            bar: {
                                dataLabels: {
                                    position: 'center', // top, center, bottom
                                },
                            }
                        },
                        dataLabels: {
                            enabled: true,
                            formatter: function (val) {
                                if (val > 0) {
                                    return val + "";
                                } else {
                                    return '';
                                }
                            },
                            style: {
                                fontSize: '11px',
                                colors: ["#304758"]
                            }
                        },
                        xaxis: {
                            categories: [],
                            type: 'datetime',
                            labels: {
                                format: 'dd/MM/yyyy',
                            },
                            axisBorder: {
                                show: false
                            },
                            axisTicks: {
                                show: false
                            },
                            title: {
                                text: i18n.t('Home.ColumnChart.Day'),
                                style: {
                                    fontSize: '15px',
                                    fontFamily: 'san-serif'
                                }
                            },
                            crosshairs: {
                                fill: {
                                    type: 'gradient',
                                    gradient: {
                                        colorFrom: '#D8E3F0',
                                        colorTo: '#BED1E6',
                                        stops: [0, 100],
                                        opacityFrom: 0.4,
                                        opacityTo: 0.5,
                                    }
                                }
                            },
                            tooltip: {
                                enabled: true,
                            }
                        },
                        responsive: [{
                            breakpoint: 480,
                            options: {
                                legend: {
                                    position: 'bottom',
                                    offsetX: -10,
                                    offsetY: 0
                                }
                            }
                        }],
                        yaxis: {
                            axisBorder: {
                                show: false
                            },
                            axisTicks: {
                                show: false,
                            },
                            labels: {
                                show: true,
                                formatter: function (val) {
                                    return val;
                                }
                            },
                            title: {
                                text: i18n.t('Home.ColumnChart.Count'),
                                style: {
                                    fontSize: '15px',
                                    fontFamily: 'san-serif'
                                }
                            },
                        },
                        tooltip: {
                            shared: false,
                            x: {
                                formatter: (value) => {
                                    return moment(value).zone('+0700').format('DD/MM/YYYY')
                                },
                            },
                        },
                    },
                },
                dataURL: null,
                gridOptions: {
                    fields: [
                        { key: 'personCode', label: "Mã nhân viên", sortable: false, },
                        { key: 'personName', label: "Nhân viên", sortable: false },
                        { key: 'deptName', label: "Phòng ban", sortable: false, },
                        { key: 'numberInOut', label: "Số lượt ra vào", sortable: false, },
                    ],
                },
            }
        },
        mounted: function () { },
        created() {
            this.loadPersonInfor();
            this.loadStatisticPersonInOut(7);
            this.loadStatisticUnregistered(7);
            this.dataURL = 'home/topEmpInOut/3';
        },
        methods: {
            loadPersonInfor() {
                return this.$services.get(`home/personInfor`).done(result => {
                    this.empWorking = result[0].empWorking;
                    this.empInactivity = result[0].empInactivity;
                    this.blacklist = result[0].blacklist;
                    this.unregistered = result[0].unregistered;
                })
            },
            refreshTopTable(options) {
                var vm = this;
                vm.dataURL = "home/topEmpInOut/" + options;
                vm.$refs.topTable.refresh();
            },
            loadStatisticPersonInOut(val) {
                var vm = this;
                var xlabels = [];
                var xseries0 = [];
                var xseries1 = [];
                return this.$services.get(`home/statisticPersonInOut/${val}`).done(result => {
                    for (var i = 0; i < result.length; i++) {
                        xseries0.push(result[i].nmbEmployee);
                        xseries1.push(result[i].nmbAll);
                        xlabels.push(result[i].dayInOut);
                        vm.$forceUpdate();
                        vm.columnChartPersonInOut.series[0].data = xseries0;
                        vm.columnChartPersonInOut.series[1].data = xseries1;
                        vm.$forceUpdate();
                        vm.$refs.columnChartPersonInOut.updateSeries([
                            {
                                name: "Nhân viên",
                                data: xseries0,
                            },
                            {
                                name: "Tổng",
                                data: xseries1,
                            },
                        ]);

                        vm.columnChartPersonInOut.chartOptions.labels = xlabels;
                        vm.$refs.columnChartPersonInOut.updateOptions(
                            {
                                xaxis: {
                                    categories: xlabels,
                                }
                            }
                        );
                        debugger
                    }
                })
            },
            loadStatisticUnregistered(val) {
                var vm = this;
                var xlabels = [];
                var xseries0 = [];
                return this.$services.get(`home/statisticUnregistered/${val}`).done(result => {
                    for (var i = 0; i < result.length; i++) {
                        xseries0.push(result[i].nmbUnregistered);
                        xlabels.push(result[i].dayInOut);
                        vm.$forceUpdate();
                        vm.columnChartUnregistered.series[0].data = xseries0;
                        vm.$forceUpdate();
                        vm.$refs.columnChartUnregistered.updateSeries([
                            {
                                name: "Chưa đăng ký",
                                data: xseries0,
                            },
                        ]);

                        vm.columnChartUnregistered.chartOptions.labels = xlabels;
                        vm.$refs.columnChartUnregistered.updateOptions(
                            {
                                xaxis: {
                                    categories: xlabels,
                                }
                            }
                        );
                    }
                })
            },
        }
    }
</script>
<style>
</style>
