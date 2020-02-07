import React from "react";
import PropTypes from "prop-types";
import {currencyFormat} from '../../commonFunctions/maths'
import { withStyles } from "@material-ui/core/styles";
import Card from "@material-ui/core/Card";
import CardActionArea from "@material-ui/core/CardActionArea";
import CardContent from "@material-ui/core/CardContent";
import CardMedia from "@material-ui/core/CardMedia";
import Typography from "@material-ui/core/Typography";
import PriceLabel from "./priceLabel";

// TODO: Tipar as props
const styles = {
  card: {
    width: 150,
    height: 210    
  },
  media: {
    height: 130,
    width: 150,
    marginTop: "5px",
    marginLeft: "0px"    
  },
  cardContent: {
    paddingRight: "10px"
  }
};

const MyCard = props => {
  const { classes, tipo, valor, isSold } = props;


  let status = isSold ? "VENDIDO" : ""
  let joia = ''
  joia = tipo == 'colar' ? 'hfpE-Colar' : joia
  joia = tipo == 'anel' ? 'Di9y-Anel' : joia
  joia = tipo == 'brinco' ? 'I7hs-Brinco' : joia
  joia = tipo == 'pulseira' ? 'M3gL-Pulseira' : joia
  
  return (
    <Card className={classes.card}>
      <CardActionArea>
        <CardMedia
          className={classes.media}                    
          image={`https://uploads.codesandbox.io/uploads/user/e8cb9cd4-720b-4761-8a65-c9f657789081/${joia}.jpg`}          
          title="jewel"
        />
        <CardContent
          className={classes.cardContent}
          >
          <Typography gutterBottom variant="headline" component="h2">
            <PriceLabel valor={currencyFormat(valor)} status={status} />
          </Typography>
          {`${tipo}`} <b>{status}</b>
        </CardContent>
      </CardActionArea>
    </Card>
  );
}

MyCard.propTypes = {
  classes: PropTypes.object.isRequired,  
  isSold: PropTypes.bool
};

export default withStyles(styles)(MyCard);
