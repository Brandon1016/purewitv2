$(document).ready(function() {


    var dom = document.getElementById("basic-Pie");
    var bpChart = echarts.init(dom);

    var app = {};
    option = null;
    option = {
        color: ['#62549a','#4aa9e9', '#ff6c60','#eac459', '#25c3b2' ],
        tooltip : {
            trigger: 'item',
            formatter: '{a} <br/>{b} : {c} ({d}%)'
        },
        legend: {
            orient : 'vertical',
            x : 'left',
            data:['Direct','Mail','Affiliate','AD','Search']
        },
        calculable : true,
        series : [
            {
                name:'Source',
                type:'pie',
                radius : '55%',
                center: ['50%', '60%'],
                data:[
                    {value:335, name:'Direct'},
                    {value:310, name:'Mail'},
                    {value:234, name:'Affiliate'},
                    {value:135, name:'AD'},
                    {value:1548, name:'Search'}
                ]
            }
        ]
    };

    if (option && typeof option === "object") {
        bpChart.setOption(option, false);
    }

   

    /**
     * Resize chart on window resize
     * @return {void}
     */
    window.onresize = function() {
        
        bpChart.resize();
        
    };


});
