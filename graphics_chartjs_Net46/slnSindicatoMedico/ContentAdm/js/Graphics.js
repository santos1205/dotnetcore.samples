$(document).ready(function () {    
    carregarGrafico();
});


        const ALTURA = 300;
        const LARGURA = 625;
        const MARGEM_TOP = 10;
        const COMECO_Y = ALTURA;

        
        //const carregarGrafico = () => {
        //    let data = [
        //        {
        //            Mes: 1,
        //            QtdeSaude: 80,
        //            QtdeOdonto: 30
        //        },
        //        {
        //            Mes: 2,
        //            QtdeSaude: 80,
        //            QtdeOdonto: 70
        //        },
        //        {
        //            Mes: 3,
        //            QtdeSaude: 90,
        //            QtdeOdonto: 100
        //        },
        //        {
        //            Mes: 4,
        //            QtdeSaude: 95,
        //            QtdeOdonto: 80
        //        },
        //        {
        //            Mes: 5,
        //            QtdeSaude: 80,
        //            QtdeOdonto: 90
        //        },
        //        {
        //            Mes: 6,
        //            QtdeSaude: 98,
        //            QtdeOdonto: 90
        //        },
        //        {
        //            Mes: 7,
        //            QtdeSaude: 80,
        //            QtdeOdonto: 85
        //        },
        //        {
        //            Mes: 8,
        //            QtdeSaude: 88,
        //            QtdeOdonto: 98
        //        },
        //        {
        //            Mes: 9,
        //            QtdeSaude: 90,
        //            QtdeOdonto: 95
        //        },
        //        {
        //            Mes: 10,
        //            QtdeSaude: 95,
        //            QtdeOdonto: 100
        //        },
        //        {
        //            Mes: 11,
        //            QtdeSaude: 20,
        //            QtdeOdonto: 90
        //        },
        //        {
        //            Mes: 12,
        //            QtdeSaude: 80,
        //            QtdeOdonto: 95
        //        },

        //    ];
        //    renderGraph(data);
        //}

        const renderGraph = (objConsolidado = null) => {
            //console.log(objConsolidado);
            
            var spaceBetween;
            objConsolidado.forEach((item) => {
                if (item.Mes === 1)
                    spaceBetween = configBarraMes(0, item.QtdeSaude, item.QtdeOdonto, item.QtdeTotal);
                else                
                    spaceBetween = configBarraMes(spaceBetween, item.QtdeSaude, item.QtdeOdonto, item.QtdeTotal);
            });            
        }
        
        const configBarraMes = (spaceBetween, vlrSaude, vlrOdonto, limiteTotal = 0) => {
            if (spaceBetween === 0)
                spaceBetween = 4;
            else
                spaceBetween += 30;

                
            // Regra de 3 para proporção entre valueY / qtde. de segurados
            // X = param qtde por plano * ALTURA Y / COUNTS SEGS ATIVOS.
            vlrSaude = (vlrSaude * ALTURA) / limiteTotal;     // 100 - valor fictício.
            vlrOdonto = (vlrOdonto * ALTURA) / limiteTotal;
            

            configBarra(20, "#045531", spaceBetween, vlrSaude, limiteTotal);
            spaceBetween += 22;
            configBarra(20, "#ff7e2b", spaceBetween, vlrOdonto, limiteTotal);
            return spaceBetween;
        }

        function configBarra(width, color, x, PosY, limiteTotal = 0) {
            this.width = width;
            this.height = PosY;
            this.x = x;
            this.y = COMECO_Y - (PosY - MARGEM_TOP);

            var c = document.getElementById("my-canvas");
            var ctx = c.getContext("2d");
            ctx.fillStyle = color;
            ctx.fillRect(this.x, this.y, this.width, this.height);
            $('#sp-limit').text(limiteTotal);
            $('#sp-half-limit').text(limiteTotal / 2);
        }