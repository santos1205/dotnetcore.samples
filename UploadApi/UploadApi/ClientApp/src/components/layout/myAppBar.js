import React from "react";
import PropTypes from "prop-types";
import { withStyles } from "@material-ui/core/styles";
import {currencyFormat} from '../../commonFunctions/maths'
import AppBar from "@material-ui/core/AppBar";
import Toolbar from "@material-ui/core/Toolbar";
import Typography from "@material-ui/core/Typography";
import IconButton from "@material-ui/core/IconButton";
import MenuIcon from "@material-ui/icons/Menu";
import Popper from "@material-ui/core/Popper";
import Grow from "@material-ui/core/Grow";
import Paper from "@material-ui/core/Paper";
import ClickAwayListener from "@material-ui/core/ClickAwayListener";
import MenuList from "@material-ui/core/MenuList";
import MenuItem from "@material-ui/core/MenuItem";
import { connect } from "react-redux";
import { bindActionCreators } from "redux";
import { actFilterJewelsByType } from "../../actions/listaKitActions";
import compose from 'recompose/compose';

const styles = theme => ({
  root: {
    flexGrow: 1,
    overflow: "hidden",
    backgroundColor: "#333",
    position: "fixed",
    top: 0,
    width: "100%",
    zIndex: 1 // sobrepor elemento
  },
  paper: {
    marginRight: theme.spacing.unit * 2
  },
  grow: {
    flexGrow: 1
  },
  menuButton: {
    marginLeft: -12,
    marginRight: 20
  }
});

class MyAppBar extends React.Component {  
  constructor( props ){
    super( props );
    this.state = {
      open: false
    };

    this.handleToggle = () => {
      this.setState(state => ({ open: !state.open }));
    };
  
    this.handleClose = event => {
      if (this.anchorEl.contains(event.target)) {
        return;
      }
      this.setState({ open: false });
    };
  }


  render() {
    const { 
      classes, 
      titulo, 
      color,      
      client,
      partialJewels,
      totalJewels,
      partialSell,      
      totalSell,
      actFilterJewelsByType
    } = this.props;
    
    const { open } = this.state;
    return (
      <div className={classes.root}>
        <AppBar color={color}>
          <Toolbar>
            <IconButton
              className={classes.menuButton}
              color="inherit"
              aria-label="Menu"
              buttonRef={node => {
                this.anchorEl = node;
              }}
              aria-owns={open ? "menu-list-grow" : null}
              aria-haspopup="true"
              onClick={this.handleToggle}
            >
              <MenuIcon />
            </IconButton>
            <Popper
              open={open}
              anchorEl={this.anchorEl}
              transition
              disablePortal
            >
              {({ TransitionProps, placement }) => (
                <Grow
                  {...TransitionProps}
                  id="menu-list-grow"
                  style={{
                    transformOrigin:
                      placement === "bottom" ? "center top" : "center bottom"
                  }}
                >
                  <Paper>
                    <ClickAwayListener onClickAway={this.handleClose}>
                      <MenuList>
                        <MenuItem onClick={() => actFilterJewelsByType('anel')}>Anéis</MenuItem>
                        <MenuItem onClick={() => actFilterJewelsByType('brinco')}>Brincos</MenuItem>
                        <MenuItem onClick={() => actFilterJewelsByType('colar')}>Colares</MenuItem>
                        <MenuItem onClick={() => actFilterJewelsByType('pulseira')}>Pulseiras</MenuItem>
                        <MenuItem onClick={() => actFilterJewelsByType('')}>Todos</MenuItem>
                      </MenuList>
                    </ClickAwayListener>
                  </Paper>
                </Grow>
              )}
            </Popper>
            <Typography
              variant="title"
              color="inherit"
              className={classes.grow}
            >
              {titulo} {client}
            </Typography>            
            <Typography color="inherit" variant="body1" gutterBottom align="right">
              {partialJewels} / {totalJewels} | R$ {currencyFormat(partialSell)} / {currencyFormat(totalSell)}
            </Typography>
          </Toolbar>
        </AppBar>
      </div>
    );
  }
}

MyAppBar.propTypes = {
  classes: PropTypes.object.isRequired
};


const mapDispatchToProps = dispatch =>
  bindActionCreators({ actFilterJewelsByType }, dispatch);

export default compose(withStyles(styles), connect(null, mapDispatchToProps))(MyAppBar);
